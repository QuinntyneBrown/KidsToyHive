import { Component, OnDestroy, Inject } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { ProductService, Product } from '@kids-toy-hive/domain';
import { baseUrl } from '@kids-toy-hive/core';
import { Router } from '@angular/router';

@Component({
  templateUrl: './toys-page.component.html',
  styleUrls: ['./toys-page.component.css'],
  selector: 'kth-toys-page',  
})
export class ToysPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    @Inject(baseUrl) public readonly _baseUrl:string,    
    private readonly _productService: ProductService,
    private readonly _router: Router
  ) {
    this.toys$ = _productService.get();
  }

  public toys$: Observable<Product[]>;

  public onGetItNowClick(toy:Product) {
    this._router.navigateByUrl('/order');
  }

  public buildImageUrl(toy: Product) {    
    return `${this._baseUrl}${toy.productImages[0].url}`;
  }
  
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
