// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, Inject, OnInit } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { ProductService, Product } from '@kids-toy-hive/domain';
import { baseUrl, LocalStorageService } from '@kids-toy-hive/core';
import { Router } from '@angular/router';

@Component({
  templateUrl: './toys-page.component.html',
  styleUrls: ['./toys-page.component.css'],
  selector: 'kth-toys-page',  
})
export class ToysPageComponent implements OnDestroy, OnInit  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public toys$: Observable<Product[]>;

  constructor(
    @Inject(baseUrl) public readonly _baseUrl:string, 
    private readonly _localStorageService: LocalStorageService,   
    private readonly _productService: ProductService,
    private readonly _router: Router
  ) {
    
  }

  ngOnInit() {
    this.toys$ = this._productService.get();
  }


  public onGetItNowClick(toy:Product) {
    this._localStorageService.put({ name: 'productId', value: toy.productId });
    this._router.navigateByUrl('/order');
  }

  public buildImageUrl(toy: Product) {    
    return `${this._baseUrl}${toy.productImages[0].url}`;
  }
  
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

