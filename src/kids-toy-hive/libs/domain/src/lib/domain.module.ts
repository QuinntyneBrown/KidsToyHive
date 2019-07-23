import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressService, OrderService, DigitalAssetService, CardService, CustomerService, DashboardCardService, DashboardService, InventoryItemService, OrderItemService, ProductCategoryService, ProductService, RoleService, UserService } from './services';
import { HttpClientModule } from '@angular/common/http';

export * from './services';

@NgModule({
  providers: [
    AddressService,
    CardService,
    CustomerService,
    DashboardCardService,
    DashboardService,
    DigitalAssetService,
    InventoryItemService,
    OrderItemService,
    OrderService,
    ProductCategoryService,
    ProductService,
    RoleService,
    UserService
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ]
})
export class DomainModule {}
