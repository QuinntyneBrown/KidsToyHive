import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { OrderService } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
  selector: 'kth-order-page'
})
export class OrderPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(private _orderService: OrderService) {

  }
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
