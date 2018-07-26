import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { AddBrandOverlayComponent } from './add-brand-overlay.component';
import { AddBrandOverlay } from './add-brand-overlay';
import { DigitalAssetsModule } from '../digital-assets/digital-assets.module';
import { BrandsPageComponent } from './brands-page.component';
import { BrandService } from './brand.service';

const declarations = [
  AddBrandOverlayComponent,
  BrandsPageComponent
];

const entryComponents = [
  AddBrandOverlayComponent
];

const providers = [
  AddBrandOverlay,
  BrandService
];

@NgModule({
  declarations,
  entryComponents,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    CoreModule,
    SharedModule,
    DigitalAssetsModule
  ],
  providers,
})
export class BrandsModule { }
