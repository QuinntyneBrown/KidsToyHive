import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { SalesOrderService } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
  selector: 'kth-order-page'
})
export class OrderPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(private _salesOrderService: SalesOrderService ) {

  }
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
