// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, Input } from '@angular/core';
import { YourOrderService } from '../your-order.service';
import { Product } from '../../../core';
import { AsyncPipe } from '@angular/common';

@Component({
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './your-order.html',
  styleUrls: ['./your-order.scss'],
  selector: 'kth-your-order'
})
export class YourOrder {   
  constructor(public readonly yourOrderService: YourOrderService) { }

  @Input()
  public product: Product;

  public get price():number {
    if(this.yourOrderService.bookingTimeSlot$.value > 1)
      return 250;
      
    return 135;    
  }
}

