import { Component, OnDestroy, Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BookingService, Booking } from '@kids-toy-hive/domain';
import { LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { takeUntil, map } from 'rxjs/operators';

@Injectable()
export class CreateBookingSectionGuard implements CanActivate {
  constructor(
    private _localStorageService: LocalStorageService,
    private _router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    const bookingId = this._localStorageService.get({ name: 'bookingId' });
    
    if(bookingId)
      return this._router.parseUrl('order/step/1');
    
    return true;
  }
}

@Component({
  templateUrl: './create-booking-section.component.html',
  styleUrls: ['./create-booking-section.component.css'],
  selector: 'kth-create-booking-section'
})
export class CreateBookingSectionComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  public form = new FormGroup({
    street: new FormControl(null, [Validators.required]),
    city: new FormControl(null, [Validators.required]),
    province: new FormControl(null, [Validators.required]),
    postalCode: new FormControl(null, [Validators.required])
  });

  constructor(
    private readonly _bookingService: BookingService,
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router,
  ) { }

  public tryToCreateBooking(booking:Booking) {
    this._bookingService.create({ booking })
    .pipe(takeUntil(this.onDestroy),map(x => { 
      this._localStorageService.put({ name: 'bookingId', value: x.bookingId });
      this._router.navigateByUrl('/order/step/3');
    }))
    .subscribe();    
  }

  ngOnDestroy() { this.onDestroy.next();	}
}
