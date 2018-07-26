import { Component } from "@angular/core";
import { Subject, BehaviorSubject } from "rxjs";
import { FormGroup, FormControl } from "@angular/forms";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { BrandService } from "./brand.service";
import { Brand } from "./brand.model";
import { map, switchMap, tap, takeUntil } from "rxjs/operators";

@Component({
  templateUrl: "./add-brand-overlay.component.html",
  styleUrls: ["./add-brand-overlay.component.css"],
  selector: "app-add-brand-overlay"
})
export class AddBrandOverlayComponent { 
  constructor(
    private _brandService: BrandService,
    private _overlay: OverlayRefWrapper) { }

  ngOnInit() {
    if (this.brandId)
      this._brandService.getById({ brandId: this.brandId })
        .pipe(
          map(x => this.brand$.next(x)),
          switchMap(x => this.brand$),
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

  public brand$: BehaviorSubject<Brand> = new BehaviorSubject(<Brand>{});
  
  public brandId: string;

  public handleCancelClick() {
    this._overlay.close();
  }

  public handleSaveClick() {
    const brand = new Brand();
    brand.brandId = this.brandId;
    brand.name = this.form.value.name;
    this._brandService.create({ brand })
      .pipe(
        map(x => brand.brandId = x.brandId),
        tap(x => this._overlay.close(brand)),
        takeUntil(this.onDestroy)
      )
      .subscribe();
  }

  public form: FormGroup = new FormGroup({
    name: new FormControl(null, [])
  });
} 
