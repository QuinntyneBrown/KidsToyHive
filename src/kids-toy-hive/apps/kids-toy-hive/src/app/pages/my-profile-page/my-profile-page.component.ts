import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { BookingService, Booking } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './my-profile-page.component.html',
  styleUrls: ['./my-profile-page.component.css'],
  selector: 'kth-my-profile-page'
})
export class MyProfilePageComponent implements OnInit, OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public bookings$:Observable<Booking[]>;

  constructor(
    private readonly _bookingService: BookingService
  ) {

  }
  
  public ngOnInit() {
    this.bookings$ = this._bookingService.getMy();
  }

  public ngOnDestroy() {
    this.onDestroy.next();	
  }
}
