import { Component, OnDestroy, Injectable, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { BookingService, Booking, AuthService } from '@kids-toy-hive/domain';
import { LocalStorageService, accessTokenKey, bookingIdKey, isProblemDetails } from '@kids-toy-hive/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { takeUntil, map } from 'rxjs/operators';
import { YourOrderService } from '../../your-order.service';

@Injectable()
export class CreateBookingSectionGuard implements CanActivate {
  constructor(
    private _localStorageService: LocalStorageService,
    private _router: Router
  ) { }
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    const bookingId = this._localStorageService.get({ name: bookingIdKey });
    const token = this._localStorageService.get({ name: accessTokenKey });

    if(!token)
      return this._router.parseUrl('order/step/1');

    if(bookingId)
      return this._router.parseUrl('order/step/3');
    
    return true;
  }
}

@Component({
  templateUrl: './create-booking-section.component.html',
  styleUrls: ['./create-booking-section.component.css'],
  selector: 'kth-create-booking-section'
})
export class CreateBookingSectionComponent implements OnInit, OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public errorMessage: string;

  private get _productId():string {
    return this._localStorageService.get({name: 'productId' })
  }
  public form = new FormGroup({
    date: new FormControl(null,[Validators.required]),
    bookingTimeSlot: new FormControl(null,[Validators.required]),
    street: new FormControl(null, [Validators.required]),
    city: new FormControl(null, [Validators.required]),
    province: new FormControl(null, [Validators.required]),
    postalCode: new FormControl(null, [Validators.required])
  });

  public bookingTimeSlots: any[] = [
    { value: 0, name:'Morning' },
    { value: 1, name:'Afternoon' },
    { value: 2, name:'Full Day' },
  ];

  constructor(
    private readonly _bookingService: BookingService,
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router,
    private readonly _authService: AuthService,
    private readonly _yourOrderService: YourOrderService
  ) { 

  }

  public ngOnInit() {
    this._yourOrderService.booking$.next(<Booking>{});
    this._authService.isAuthenticatedChanged$
    .pipe(takeUntil(this.onDestroy),map(x => {
      if(!x) {        
        this._router.navigateByUrl('order/step/1');
      }
    }))
    .subscribe();
  }

  public handleBookingTimeSlotChange() {
    this._yourOrderService.bookingTimeSlot$.next(this.form.value.bookingTimeSlot);
  }

  public handleBookingDateChange() {
    this._yourOrderService.bookingDate$.next(this.form.value.date);
  }

  public tryToCreateBooking(value:any) {
    const booking: Booking = {
      bookingDetails:[
        { 
          productId: this._productId,
          quantity:1
        }
      ],
      date: value.date,
      bookingTimeSlot:value.bookingTimeSlot,
      location : {
        address: {
          street: value.street,
          city: value.city,
          province: value.province,
          postalCode: value.postalCode
        }
      }
    };
    this._bookingService.create({ booking })
    .pipe(takeUntil(this.onDestroy),map((x:any) => { 
      if(isProblemDetails(x)) {
        this.errorMessage = 'The Jungle Jumparro is unavaliable for the time and date you have selected';
      } else {
        this._localStorageService.put({ name: bookingIdKey, value: x.bookingId });
        this._router.navigateByUrl('/order/step/3');
      }
    }))
    .subscribe();    
  }

  ngOnDestroy() { this.onDestroy.next();	}
}
