import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { ShipmentSalesOrderService } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './orders-page.component.html',
  styleUrls: ['./orders-page.component.css'],
  selector: 'kth-orders-page'
})
export class OrdersPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    private readonly _ordersService: ShipmentSalesOrderService
  ) {

  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
