import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressService, DigitalAssetService, CardService, CustomerService, DashboardCardService, DashboardService, InventoryItemService, ProductCategoryService, ProductService, RoleService, UserService, SalesOrderService, SalesOrderDetailService } from './services';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard, LoginRedirectService, AuthService } from './guards';
import { ShipmentSalesOrderService } from './services/shipment-sales-order.service';

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
    SalesOrderDetailService,
    SalesOrderService,
    ShipmentSalesOrderService,
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
