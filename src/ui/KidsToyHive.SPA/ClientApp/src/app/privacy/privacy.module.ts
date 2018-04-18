import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivacyPageComponent } from './privacy-page.component';

const declarations = [
  PrivacyPageComponent
];

@NgModule({
  imports: [
    CommonModule
  ],
  declarations,
  exports: declarations
})
export class PrivacyModule { }
