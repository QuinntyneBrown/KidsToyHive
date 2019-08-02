import { Component, OnDestroy, Input } from '@angular/core';
import { Subject } from 'rxjs';
import { Booking } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './my-booking.component.html',
  styleUrls: ['./my-booking.component.css'],
  selector: 'kth-my-booking'
})
export class MyBookingComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Input()
  public booking: Booking;

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
