import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from "@angular/common/http";
import { FooterComponent } from './footer.component';
import { HeaderComponent } from './header.component';
import { RouterModule } from '@angular/router';
import { ProductService } from './services/product.service';
import { ProductCategoryService } from './services/product-category.service';

const declarations = [
  FooterComponent,
  HeaderComponent
];

const providers = [
  ProductService,
  ProductCategoryService
];

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule
  ],
  declarations,
  exports: declarations,
  providers
})
export class SharedModule { }
