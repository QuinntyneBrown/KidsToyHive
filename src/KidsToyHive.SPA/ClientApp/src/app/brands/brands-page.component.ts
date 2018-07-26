import { Component } from "@angular/core";
import { Subject, BehaviorSubject } from "rxjs";
import { AddBrandOverlay } from "./add-brand-overlay";
import { BrandService } from "./brand.service";
import { Brand } from "./brand.model";
import { tap, takeUntil } from "rxjs/operators";

@Component({
  templateUrl: "./brands-page.component.html",
  styleUrls: ["./brands-page.component.css"],
  selector: "app-brands-page"
})
export class BrandsPageComponent { 
  constructor(private _addBrandOverlay: AddBrandOverlay, private _brandService: BrandService) {

  }
  
  ngOnInit() {
    this._brandService.get()
      .pipe(takeUntil(this.onDestroy), tap(x => this.brands$.next(x)))
      .subscribe();
  }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();    
  }

  public brands$: BehaviorSubject<Brand[]> = new BehaviorSubject([]);
}
