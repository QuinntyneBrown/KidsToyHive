import { Component, OnDestroy, Input, Inject } from '@angular/core';
import { Subject } from 'rxjs';
import { Product } from '@kids-toy-hive/domain';
import { baseUrl } from '@kids-toy-hive/core';
import { YourOrderService } from './your-order.service';


@Component({
  templateUrl: './your-order.component.html',
  styleUrls: ['./your-order.component.css'],
  selector: 'kth-your-order'
})
export class YourOrderComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    @Inject(baseUrl) private readonly _baseUrl:string,
    public readonly yourOrderService: YourOrderService
  ) {

  }
  @Input()
  public product: Product;

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public get price():number {
    if(this.yourOrderService.bookingTimeSlot$.value > 1) {
      return 250;
    } else {
      return 135;
    }
  }

  public get baseUrl() { return this._baseUrl; }

}
