import { Component } from "@angular/core";
import { Subject, BehaviorSubject } from "rxjs";
import { FormGroup, FormControl } from "@angular/forms";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { ProductService } from "./product.service";
import { Product } from "./product.model";
import { map, switchMap, tap, takeUntil } from "rxjs/operators";

@Component({
  templateUrl: "./add-product-overlay.component.html",
  styleUrls: ["./add-product-overlay.component.css"],
  selector: "app-add-product-overlay",
  host: { 'class': 'mat-typography' }
})
export class AddProductOverlayComponent { 
  constructor(
    private _productService: ProductService,
    private _overlay: OverlayRefWrapper) { }

  ngOnInit() {
    if (this.productId)
      this._productService.getById({ productId: this.productId })
        .pipe(
          map(x => this.product$.next(x)),
          switchMap(x => this.product$),
          map(x => this.form.patchValue({
            name: x.name
          }))
        )
        .subscribe();
  }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public product$: BehaviorSubject<Product> = new BehaviorSubject(<Product>{});
  
  public productId: string;

  public handleCancelClick() {
    this._overlay.close();
  }

  public handleSaveClick() {
    const product = new Product();
    product.productId = this.productId;
    product.name = this.form.value.name;
    this._productService.create({ product })
      .pipe(
        map(x => product.productId = x.productId),
        tap(x => this._overlay.close(product)),
        takeUntil(this.onDestroy)
      )
      .subscribe();
  }

  public form: FormGroup = new FormGroup({
    name: new FormControl(null, []),
    imageUrl: new FormControl(null, [])
  });
} 
