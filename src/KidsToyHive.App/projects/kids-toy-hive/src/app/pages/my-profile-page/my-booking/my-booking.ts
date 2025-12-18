// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, Input, Inject } from '@angular/core';
import { Subject } from 'rxjs';
import { Booking } from '../../../core';
import { baseUrl } from '../../../core';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';

@Component({
  standalone: true,
  imports: [DatePipe],
  templateUrl: './my-booking.html',
  styleUrls: ['./my-booking.scss'],
  selector: 'kth-my-booking'
})
export class MyBooking implements OnDestroy  { 
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

