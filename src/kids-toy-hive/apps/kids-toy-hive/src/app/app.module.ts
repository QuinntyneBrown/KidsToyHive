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

    TermsAndConditionsPageComponent
  ],
  imports: [
    AppRoutingModule,

    CoreModule,
    DomainModule,
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: baseUrl, useValue: environment.baseUrl },

    CreateCustomerSectionGuard,
    CreateBookingSectionGuard,
    ProcessPaymentSectionGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
