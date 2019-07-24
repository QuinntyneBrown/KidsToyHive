import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { ProductService } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './catalog-page.component.html',
  styleUrls: ['./catalog-page.component.css'],
  selector: 'kth-catalog-page'
})
export class CatalogPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(private readonly _productService: ProductService) {

  }
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
