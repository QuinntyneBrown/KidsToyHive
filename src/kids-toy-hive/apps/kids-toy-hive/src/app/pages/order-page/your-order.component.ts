import { Component, OnDestroy, Input, Inject } from '@angular/core';
import { Subject } from 'rxjs';
import { Product } from '@kids-toy-hive/domain';
import { baseUrl } from '@kids-toy-hive/core';
import { OrderPageService } from './order-page-service';

@Component({
  templateUrl: './your-order.component.html',
  styleUrls: ['./your-order.component.css'],
  selector: 'kth-your-order'
})
export class YourOrderComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    @Inject(baseUrl) private readonly _baseUrl:string,
    public readonly orderPageService: OrderPageService
  ) {

  }
  @Input()
  public product: Product;

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public get baseUrl() { return this._baseUrl; }

  public getImageUrl() {    
    return `${this.baseUrl}${this.product.productImages[0].url}`;
  }
}
