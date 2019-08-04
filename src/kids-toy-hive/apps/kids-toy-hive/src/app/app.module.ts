import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AboutPageComponent, HomePageComponent, OrderPageComponent, ConfirmationPageComponent, CreateCustomerSectionComponent, CreateBookingSectionComponent, ProcessBookingPaymentComponent, CreateBookingSectionGuard, ProcessPaymentSectionGuard, CreateCustomerSectionGuard, JoinNowComponent, HowItWorksComponent, TestimonialsComponent, YourOrderComponent } from './pages';
import { AppRoutingModule } from './app-routing.module';
import { CoreModule, baseUrl } from '@kids-toy-hive/core';
import { DomainModule } from '@kids-toy-hive/domain';
import { SharedModule } from '@kids-toy-hive/shared';
import { environment } from '../environments/environment';
import { TermsAndConditionsPageComponent } from './pages/terms-and-conditions-page';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToysPageComponent, ToyComponent } from './pages/toys-page';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule, MatNativeDateModule } from '@angular/material';
import { MyProfilePageComponent, MyBookingComponent } from './pages/my-profile-page';
import { FeaturesSecurityModule } from '@kids-toy-hive/features/security';
import { MenuOverlayComponent, MenuOverlay } from './overlays';
import { MenuComponent } from './overlays/menu.component';
import { YourOrderService } from './pages/order-page/your-order.service';

@NgModule({
  declarations: [
    AppComponent,
    AboutPageComponent,
    ToysPageComponent,
    ToyComponent,
    ConfirmationPageComponent,
    HomePageComponent,

    HowItWorksComponent,
    JoinNowComponent,
    TestimonialsComponent,
    
    OrderPageComponent,
    YourOrderComponent,
    CreateBookingSectionComponent,    
    CreateCustomerSectionComponent,
    ProcessBookingPaymentComponent,

    TermsAndConditionsPageComponent,
    
    MyProfilePageComponent,

    MyBookingComponent,

    MenuOverlayComponent,
    MenuComponent
  ],
  entryComponents: [
    MenuOverlayComponent
  ],  
  imports: [
    AppRoutingModule,

    CoreModule,
    DomainModule,
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,

    MatDatepickerModule,
    MatNativeDateModule,

    FeaturesSecurityModule
  ],
  providers: [
    { provide: baseUrl, useValue: environment.baseUrl },
    
    CreateCustomerSectionGuard,
    CreateBookingSectionGuard,
    ProcessPaymentSectionGuard,

    MenuOverlay,
    YourOrderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
