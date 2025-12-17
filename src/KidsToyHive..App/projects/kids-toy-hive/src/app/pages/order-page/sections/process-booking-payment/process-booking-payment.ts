// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, Injectable, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { CustomerService, Customer, BookingService, AuthService } from '../../../../core';
import { LocalStorageService, accessTokenKey, bookingIdKey, productIdKey, isProblemDetails, ProblemDetails } from '../../../../core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { takeUntil, map } from 'rxjs/operators';
import { Button } from '../../../../components/button/button';

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
  standalone: true,
  imports: [ReactiveFormsModule, Button],
  templateUrl: './process-booking-payment.html',
  styleUrls: ['./process-booking-payment.scss'],
  selector: 'kth-process-booking-payment'
})
export class ProcessBookingPayment implements OnInit, OnDestroy  { 
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

  get number() { return this.form.get('number'); }

  get expMonth() { return this.form.get('expMonth'); }

  get expYear() { return this.form.get('expYear'); }

  get cvc() { return this.form.get('cvc'); }
}

