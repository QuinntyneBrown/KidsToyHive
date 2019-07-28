import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { ProductService } from '@kids-toy-hive/domain';
import { Product } from 'libs/domain/src/lib/models';

@Component({
  templateUrl: './catalog-page.component.html',
  styleUrls: ['./catalog-page.component.css'],
  selector: 'kth-catalog-page'
})
export class CatalogPageComponent implements OnInit, OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public products$:Observable<Product[]>;

  constructor(private readonly _productService: ProductService) {

  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  ngOnInit() {
    
    //this.products$ = this._productService.get();

    this._productService.get().subscribe();
  }
}
