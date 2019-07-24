import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './orders-page.component.html',
  styleUrls: ['./orders-page.component.css'],
  selector: 'kth-orders-page'
})
export class OrdersPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
