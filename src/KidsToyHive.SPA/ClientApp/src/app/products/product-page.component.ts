import { Component, ViewChild } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";
import { takeUntil, tap } from "rxjs/operators";
import { AddProductOverlay } from "./add-product-overlay";
import { Product } from "./product.model";
import { ProductService } from "./product.service";
import { IgxGridComponent } from "igniteui-angular";

@Component({
  templateUrl: "./product-page.component.html",
  styleUrls: ["./product-page.component.css"],
  selector: "app-product-page"
})
export class ProductPageComponent { 
  constructor(private _addProductOverlay: AddProductOverlay, private _productService: ProductService) { }

  ngOnInit() {
    this._productService.get()
      .pipe(takeUntil(this.onDestroy), tap(x => this.products$.next(x)))
      .subscribe();
  }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();    
  }
  columns: any[] = [
    {
      field: "name",
      header: "Name"
    }
  ];

  afterViewInit: boolean;

  ngAfterViewInit() {
    this.afterViewInit = true;
  }
  ngDoCheck() {
    if (this.grid && this.afterViewInit)
      this.grid.reflow();    
  }

  @ViewChild("grid")
  public grid: IgxGridComponent;

  public openOverlay() {
    this._addProductOverlay.create();
  }

  public products$: BehaviorSubject<Product[]> = new BehaviorSubject([]);
}
