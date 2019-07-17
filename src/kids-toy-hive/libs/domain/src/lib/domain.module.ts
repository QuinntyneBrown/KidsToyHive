import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressService } from './services';

export * from './services';

@NgModule({
  providers: [
    AddressService
  ],
  imports: [
    CommonModule
  ]
})
export class DomainModule {}
