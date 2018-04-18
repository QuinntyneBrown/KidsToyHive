import { Component, Input } from "@angular/core";
import { Subject } from "rxjs";
import { ProductCategory } from "../shared/models/product-category.model";

@Component({
  templateUrl: "./toy-categories-grid.component.html",
  styleUrls: ["./toy-categories-grid.component.css"],
  selector: "app-toy-categories-grid"
})
export class ToyCategoriesGridComponent { 

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  @Input()
  public categories: Array<ProductCategory> = [];

}
