// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductService, Product } from '@kids-toy-hive/domain';
import { Observable, Subject } from 'rxjs';
import { LocalStorageService } from '@kids-toy-hive/core';
import { takeUntil, map } from 'rxjs/operators';
import { YourOrderService } from './your-order.service';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';

export * from './sections';

@Component({
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
  selector: 'kth-order-page'
})
export class OrderPageComponent implements OnInit, OnDestroy { 

  public product$: Observable<Product>;
  public isMobile:boolean;

  private readonly _destroy: Subject<void> = new Subject();
  constructor(
    private readonly _localStorageService: LocalStorageService,
    private readonly _yourOrderService: YourOrderService,
    private readonly _productService: ProductService,
    private readonly _breakpointObserver: BreakpointObserver
  ) { }

  public ngOnInit() {

    const productId = this._localStorageService.get({ name: 'productId' });
    
    this._productService.getById({ productId })
    .pipe(takeUntil(this._destroy), map(x => {
      this._yourOrderService.product$.next(x);
    }))
    .subscribe();

    this._breakpointObserver
    .observe(['(min-width: 768px)'])
    .subscribe((state: BreakpointState) => {
      this.isMobile = !state.matches;
    });

  }

  public ngOnDestroy() {
    this._destroy.next();
  }
}

