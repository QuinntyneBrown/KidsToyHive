import { Component, OnDestroy } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { CustomerService, BookingService, Customer, Booking, LocationService, Location } from '@kids-toy-hive/domain';
import { LocalStorageService } from '@kids-toy-hive/core';
import { takeUntil, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
  selector: 'kth-order-page'
})
export class OrderPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    private readonly _customerService: CustomerService,
    private readonly _bookingService: BookingService,
    private readonly _localStorageService: LocalStorageService,
    private readonly _locationService: LocationService,
    private readonly _router: Router   
  ) { }

  private readonly _customer: Customer;
  private readonly _booking: Booking;
  private readonly _deliveryLocation: Location;
  private readonly _pickUpLocation: Location;
  public number: number;
  public expMonth: number;
  public expYear: number;
  public cvc:string;

  public tryToProcessBookingPayment() {    
    this._bookingService.processBookingPayment({
      number : this.number,
      expMonth : this.expMonth,
      expYear : this.expYear,
      cvc : this.cvc,
      bookingId : this._booking.bookingId
    }).subscribe();    
  }

  
  public tryToSaveCustomer() {
    this._customerService.create({ customer: this._customer})
    .pipe(takeUntil(this.onDestroy))
    .subscribe(x => { 
      this._customer.customerId = x.customerId;
      this._customer.version = x.version;
    });    
  }

  public tryToSaveBooking() {
    this._bookingService.create({ booking: this._booking})
    .pipe(takeUntil(this.onDestroy))
    .subscribe(x => { 
        this._booking.bookingId = x.bookingId;
        this._booking.version = x.version;
    });    
  }

  public tryToSaveDeliveryLocation() {
    this.tryToSaveLocation(this._deliveryLocation);
  }

  public tryToSavePickUpLocation() {
    this.tryToSaveLocation(this._pickUpLocation);
  }

  public tryToSaveLocation(location: Location):Observable<Location> {
    return this._locationService.create({ location })
    .pipe(takeUntil(this.onDestroy),map(x => {
      location.locationId = x.locationId;
      location.version = x.version;
      return location;
    }))
  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
