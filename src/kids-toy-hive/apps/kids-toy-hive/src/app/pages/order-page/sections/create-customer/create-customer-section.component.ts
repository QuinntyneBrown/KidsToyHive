import { Component, OnDestroy, Injectable, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { CustomerService, Customer, AuthService } from '@kids-toy-hive/domain';
import { LocalStorageService, accessTokenKey, isProblemDetails } from '@kids-toy-hive/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { takeUntil, map } from 'rxjs/operators';

@Injectable()
export class CreateCustomerSectionGuard implements CanActivate {
  constructor(
    private _localStorageService: LocalStorageService,
    private _router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    const token = this._localStorageService.get({ name: accessTokenKey });
    const productId = this._localStorageService.get({ name: 'productId' });
    
    if(!productId)
      return this._router.parseUrl('toys');

    if(token)
      return this._router.parseUrl('order/step/2');
    
    return true;
  }
}

@Component({
  templateUrl: './create-customer-section.component.html',
  styleUrls: ['./create-customer-section.component.css'],
  selector: 'kth-create-customer-section'
})
export class CreateCustomerSectionComponent implements OnInit, OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public errorMessage: string;
  public form = new FormGroup({
    firstName: new FormControl(null, [Validators.required]),
    lastName: new FormControl(null, [Validators.required]),
    phoneNumber: new FormControl(null, [Validators.required]),
    email: new FormControl(null, [Validators.required, Validators.email])
  });

  constructor(
    private readonly _authService: AuthService,    
    private readonly _customerService: CustomerService,
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router,
  ) { }

  public ngOnInit() {
    this._localStorageService.changes$
    .pipe(takeUntil(this.onDestroy),map(x => {
      if(this._localStorageService.get({ name: accessTokenKey }))
        this._router.navigateByUrl('order/step/2');
    }))
    .subscribe()  
  }

  public tryToSaveCustomer(customer:any) {

    if(this.form.valid) {
      this._customerService.create({ customer})
      .pipe(takeUntil(this.onDestroy),map(x => {
        
        if(isProblemDetails(x)) {
          this.errorMessage = 'An account exist with this email. Sign in with the existing account.'
        } 
        else 
        {
          this._localStorageService.put({ name: accessTokenKey, value: (<any>x).accessToken });
          this._router.navigateByUrl('/order/step/2');
          this._authService.isAuthenticatedChanged$.next(true);
        }
      }))
      .subscribe();    
    }
  }

  get firstName() { return this.form.get('firstName'); }

  get lastName() { return this.form.get('lastName'); }

  get email() { return this.form.get('email'); }

  get phoneNumber() { return this.form.get('phoneNumber'); }

  ngOnDestroy() { this.onDestroy.next();	}
}
