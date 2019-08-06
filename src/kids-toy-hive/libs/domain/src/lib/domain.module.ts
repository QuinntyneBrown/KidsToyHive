import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressService, DigitalAssetService, CardService, CustomerService, DashboardCardService, DashboardService, InventoryItemService, ProductCategoryService, ProductService, RoleService, UserService, SalesOrderService, SalesOrderDetailService, BookingService, LocationService, HtmlContentService } from './services';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthGuard, LoginRedirectService, AuthService } from './guards';
import { ShipmentSalesOrderService } from './services/shipment-sales-order.service';
import { AuthInterceptor } from './interceptors';
import { ErrorService } from './services/error.service';

export * from './models';
export * from './guards';
export * from './services';

@NgModule({
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },

    ErrorService,
    
    AuthService,
    AuthGuard,
    LoginRedirectService,

    AddressService,
    BookingService,
    CardService,
    CustomerService,
    DashboardCardService,
    DashboardService,
    DigitalAssetService,
    HtmlContentService,
    InventoryItemService,
    LocationService,
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
