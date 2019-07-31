import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AboutPageComponent, HomePageComponent, OrderPageComponent, ConfirmationPageComponent, CreateCustomerSectionComponent, CreateBookingSectionComponent, ProcessBookingPaymentComponent, CreateBookingSectionGuard, ProcessPaymentSectionGuard, CreateCustomerSectionGuard } from './pages';
import { AppRoutingModule } from './app-routing.module';
import { CoreModule, baseUrl } from '@kids-toy-hive/core';
import { DomainModule } from '@kids-toy-hive/domain';
import { SharedModule } from '@kids-toy-hive/shared';
import { environment } from '../environments/environment';
import { TermsAndConditionsPageComponent } from './pages/terms-and-conditions-page';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToysPageComponent, ToyComponent } from './pages/toys-page';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material';
import { DoneSectionComponent } from './pages/order-page/sections/done';
import { MyProfilePageComponent } from './pages/my-profile-page';
import { FeaturesSecurityModule } from '@kids-toy-hive/features/security';
import { MenuOverlayComponent, MenuOverlay } from './overlays';


@NgModule({
  declarations: [
    AppComponent,
    AboutPageComponent,
    ToysPageComponent,
    ToyComponent,
    ConfirmationPageComponent,
    HomePageComponent,
    OrderPageComponent,

    CreateBookingSectionComponent,    
    CreateCustomerSectionComponent,
    ProcessBookingPaymentComponent,
    DoneSectionComponent,

    TermsAndConditionsPageComponent,
    MyProfilePageComponent,
    MenuOverlayComponent
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

    FeaturesSecurityModule
  ],
  providers: [
    { provide: baseUrl, useValue: environment.baseUrl },
    
    CreateCustomerSectionGuard,
    CreateBookingSectionGuard,
    ProcessPaymentSectionGuard,

    MenuOverlay
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
