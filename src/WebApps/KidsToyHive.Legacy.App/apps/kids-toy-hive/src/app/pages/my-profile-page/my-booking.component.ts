import { Component, OnDestroy, Input, Inject } from '@angular/core';
import { Subject } from 'rxjs';
import { Booking } from '@kids-toy-hive/domain';
import { baseUrl } from '@kids-toy-hive/core';
import * as moment from 'moment';

@Component({
  templateUrl: './my-booking.component.html',
  styleUrls: ['./my-booking.component.css'],
  selector: 'kth-my-booking'
})
export class MyBookingComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    @Inject(baseUrl) private readonly _baseUrl:string
  ) {

  }

  public get baseUrl() {
    return this._baseUrl;
  }
  
  @Input()
  public booking: Booking;

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public formatDate(date) {
    return moment(date).format('dddd MMMM D, YYYY');
  }
}
