import { Component, OnDestroy, Injectable, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { CustomerService, Customer, BookingService, AuthService } from '@kids-toy-hive/domain';
import { LocalStorageService, accessTokenKey, bookingIdKey, productIdKey, isProblemDetails, ProblemDetails } from '@kids-toy-hive/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { takeUntil, map } from 'rxjs/operators';

@Injectable()
export class ProcessPaymentSectionGuard implements CanActivate {
  constructor(
    private _localStorageService: LocalStorageService,
    private _router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {    
    const bookingId = this._localStorageService.get({ name: 'bookingId' });
    const token = this._localStorageService.get({ name: accessTokenKey });

    if(!bookingId || !token)
      return this._router.parseUrl('order/step/2');

    return true;
  }
}

@Component({
  templateUrl: './process-booking-payment.component.html',
  styleUrls: ['./process-booking-payment.component.css'],
  selector: 'kth-process-booking-payment'
})
export class ProcessBookingPaymentComponent implements OnInit, OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public errorMessage: string;
  public form = new FormGroup({
    number: new FormControl(null, [Validators.required]),
    expYear: new FormControl(null, [Validators.required]),
    expMonth: new FormControl(null, [Validators.required]),
    cvc: new FormControl(null, [Validators.required])
  });

  constructor(
    private readonly _authService: AuthService,
    private readonly _bookingService: BookingService,
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router
  ) { }

  ngOnInit() {
    this._authService.isAuthenticatedChanged$
    .pipe(takeUntil(this.onDestroy),map(x => {      
      if(!x) { 
        this._router.navigateByUrl('order/step/1'); 
      }
    }))
    .subscribe();
  }
  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public tryToCheckout(formValue:any) {
    const bookingId = this._localStorageService.get({ name: bookingIdKey });
    this._bookingService.processBookingPayment({ 
      number: formValue.number,
      expMonth: formValue.expMonth,
      expYear: formValue.expYear,
      cvc: formValue.cvc,
      bookingId
    })
    .pipe(takeUntil(this.onDestroy),map((x:ProblemDetails) => {       
      if(isProblemDetails(x)) {
        this.errorMessage = x.detail;
      } else {
        this._localStorageService.remove({ name: bookingIdKey });
        this._localStorageService.remove({ name: productIdKey });
        this._router.navigateByUrl('myprofile');
      }
    }))
    .subscribe(); 
  }
}
