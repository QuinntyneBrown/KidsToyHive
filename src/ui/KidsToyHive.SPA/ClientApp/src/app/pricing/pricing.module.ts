import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PricingPageComponent } from './pricing-page.component';
import { SharedModule } from '../shared/shared.module';

const declarations = [
  PricingPageComponent
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations,
  exports:declarations
})
export class PricingModule { }
