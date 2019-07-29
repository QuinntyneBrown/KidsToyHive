import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';

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
