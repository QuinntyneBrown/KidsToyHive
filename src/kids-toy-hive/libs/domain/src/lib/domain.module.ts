import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressService, OrderService, DigitalAssetService, CardService, CustomerService, DashboardCardService, DashboardService, InventoryItemService, OrderItemService, ProductCategoryService, ProductService, RoleService, UserService } from './services';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard, LoginRedirectService, AuthService } from './guards';

export * from './guards';
export * from './services';

@NgModule({
  providers: [
    AuthService,
    AuthGuard,
    LoginRedirectService,

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
