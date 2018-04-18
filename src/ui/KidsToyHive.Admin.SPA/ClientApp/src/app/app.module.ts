import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { AppComponent } from './app.component';
import { AppRouterModule } from './app-router.module';
import { CardsModule } from './cards/cards.module';
import { DashboardCardsModule } from './dashboard-cards/dashboard-cards.module';
import { DashboardsModule } from './dashboards/dashboards.module';
import { LoginModule } from './login/login.module';
import { MaterialModule } from './material/material.module';
import { ProductsModule } from './products/products.module';
import { SharedModule } from './shared/shared.module';
import { AnonymousMasterPageComponent } from './anonymous-master-page.component';
import { MasterPageComponent } from './master-page.component';
import { DigitalAssetsModule } from './digital-assets/digital-assets.module';

@NgModule({
  declarations: [
    AppComponent,
    AnonymousMasterPageComponent,
    MasterPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,

    AppRouterModule,

    CardsModule,
    DashboardCardsModule,
    DashboardsModule,
    DigitalAssetsModule,
    LoginModule,
    MaterialModule,
    ProductsModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
