import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { AddProductOverlay } from './add-product-overlay';
import { ProductService } from './product.service';
import { AddProductOverlayComponent } from './add-product-overlay.component';
import { DigitalAssetsModule } from '../digital-assets/digital-assets.module';
import { ProductPageComponent } from './product-page.component';

const declarations = [
  AddProductOverlayComponent,
  ProductPageComponent
];

const entryComponents = [
  AddProductOverlayComponent
];

const providers = [
  AddProductOverlay,
  ProductService
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
    DigitalAssetsModule,
    SharedModule    
  ],
  providers,
})
export class ProductsModule { }
