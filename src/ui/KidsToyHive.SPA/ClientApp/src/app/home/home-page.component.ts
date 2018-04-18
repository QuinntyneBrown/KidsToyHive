import { Component } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { ProductCategoryService } from "../shared/services/product-category.service";
import { ProductCategory } from "../shared/models/product-category.model";
import { map } from "rxjs/operators";

@Component({
  templateUrl: "./home-page.component.html",
  styleUrls: ["./home-page.component.css"],
  selector: "app-home-page"
})
export class HomePageComponent { 
  constructor(private _productCategoryService: ProductCategoryService) {

  }

  public ngOnInit() {
    this.categories$ = this._productCategoryService.get();
  }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public categories$: Observable<Array<ProductCategory>>;
}
