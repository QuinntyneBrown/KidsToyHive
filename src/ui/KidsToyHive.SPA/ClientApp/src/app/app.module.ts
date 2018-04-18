import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeModule } from './home/home.module';
import { SharedModule } from './shared/shared.module';
import { PrivacyModule } from './privacy/privacy.module';
import { ToysModule } from './toys/toys.module';
import { AboutModule } from './about/about.module';
import { ShoppingCartModule } from './shopping-cart/shopping-cart.module';
import { PricingModule } from './pricing/pricing.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,

    AboutModule,
    HomeModule,
    PricingModule,
    PrivacyModule,
    SharedModule,
    ShoppingCartModule,
    ToysModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
