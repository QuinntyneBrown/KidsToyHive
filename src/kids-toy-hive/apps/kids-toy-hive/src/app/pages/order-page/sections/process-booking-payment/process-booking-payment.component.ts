import { Component, OnDestroy, Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { LocalStorageService } from '@kids-toy-hive/core';

@Injectable()
export class ProcessPaymentSectionGuard implements CanActivate {
  constructor(
    private _localStorageService: LocalStorageService,
    private _router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    const bookingId = this._localStorageService.get({ name: 'bookingId' });
    
    if(!bookingId)
      return this._router.parseUrl('order/step/2');
    
    return true;
  }
}

@Component({
  templateUrl: './process-booking-payment.component.html',
  styleUrls: ['./process-booking-payment.component.css'],
  selector: 'kth-process-booking-payment'
})
export class ProcessBookingPaymentComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    private readonly _router: Router
  ) {

  }
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
