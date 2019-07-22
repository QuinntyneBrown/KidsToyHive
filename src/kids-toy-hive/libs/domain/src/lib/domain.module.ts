import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressService, OrderService, DigitalAssetService } from './services';
import { HttpClientModule } from '@angular/common/http';

export * from './services';

@NgModule({
  providers: [
    AddressService,
    DigitalAssetService,
    OrderService
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ]
})
export class DomainModule {}
