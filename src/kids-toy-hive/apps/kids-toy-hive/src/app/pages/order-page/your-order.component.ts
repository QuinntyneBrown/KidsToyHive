import { Component, Input } from '@angular/core';
import { Product } from '@kids-toy-hive/domain';
import { YourOrderService } from './your-order.service';

@Component({
  templateUrl: './your-order.component.html',
  styleUrls: ['./your-order.component.css'],
  selector: 'kth-your-order'
})
export class YourOrderComponent {   
  constructor(public readonly yourOrderService: YourOrderService) { }

  @Input()
  public product: Product;

  public get price():number {
    if(this.yourOrderService.bookingTimeSlot$.value > 1) {
      return 250;
    } else {
      return 135;
    }
  }
}
