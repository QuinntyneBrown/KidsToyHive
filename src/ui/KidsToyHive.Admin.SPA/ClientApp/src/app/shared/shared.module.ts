import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRedirect } from './login-redirect';
import { Storage } from './storage';
import { AccountService } from './services/account.service';
import { AddressService } from './services/address.service';
import { AuthenticationService } from './services/authentication.service';
import { BrandService } from './services/brand.service';
import { CardService } from './services/card.service';
import { ContactService } from './services/contact.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ContactRequestService } from './services/contact-request.service';
import { ContentService } from './services/content.service';
import { ConversationService } from './services/conversation.service';
import { DashboardService } from './services/dashboard.service';
import { DashboardCardService } from './services/dashboard-card.service';
import { DigitalAssetService } from './services/digital-asset.service';
import { MessageService } from './services/message.service';
import { ProductService } from './services/product.service';
import { ProductCategoryService } from './services/product-category.service';
import { ProductImageService } from './services/product-image.service';
import { UserService } from './services/user.service';
import { HeaderInterceptor } from './header.interceptor';

@NgModule({
  imports: [
    HttpClientModule,
    CommonModule
  ],
  declarations: [],
  providers: [
    LoginRedirect,
    Storage,

    {
      provide: HTTP_INTERCEPTORS,
      useClass: HeaderInterceptor,
      multi:true
    },

    AccountService,
    AddressService,
    AuthenticationService,
    BrandService,
    CardService,
    ContactService,
    ContactRequestService,
    ContentService,
    ConversationService,
    DashboardService,
    DashboardCardService,
    DigitalAssetService,
    MessageService,
    ProductService,
    ProductCategoryService,
    ProductImageService,
    UserService
  ]
})
export class SharedModule { }
