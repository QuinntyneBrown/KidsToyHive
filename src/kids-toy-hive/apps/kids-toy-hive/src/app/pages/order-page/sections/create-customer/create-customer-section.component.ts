import { Component, OnDestroy, Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { CustomerService, Customer } from '@kids-toy-hive/domain';
import { LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';
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
export class CreateCustomerSectionComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  public form = new FormGroup({
    firstName: new FormControl(null, [Validators.required]),
    lastName: new FormControl(null, [Validators.required]),
    phoneNumber: new FormControl(null, [Validators.required]),
    email: new FormControl(null, [Validators.required, Validators.email])
  });

  constructor(
    private readonly _customerService: CustomerService,
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router,
  ) { }

  public tryToSaveCustomer(customer:any) {
    this._customerService.create({ customer})
    .pipe(takeUntil(this.onDestroy),map(x => { 
      this._localStorageService.put({ name: accessTokenKey, value: x.accessToken });
      this._router.navigateByUrl('/order/step/2');
    }))
    .subscribe();    
  }

  ngOnDestroy() { this.onDestroy.next();	}
}
