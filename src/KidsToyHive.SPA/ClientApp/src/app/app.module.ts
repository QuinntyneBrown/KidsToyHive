import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { AnonymousMasterPageComponent } from './anonymous-master-page.component';
import { MasterPageComponent } from './master-page.component';
import { AppRoutingModule } from './app-routing.module';
import { UsersModule } from './users/users.module';
import { baseUrl } from './core/constants';
import { CardsModule } from './cards/cards.module';
import { DashboardCardsModule } from './dashboard-cards/dashboard-cards.module';
import { DashboardsModule } from './dashboards/dashboards.module';
import { ProductsModule } from './products/products.module';
import { BrandsModule } from './brands/brands.module';

@NgModule({
  declarations: [
    AppComponent,
    AnonymousMasterPageComponent,
    MasterPageComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    AppRoutingModule,

    BrandsModule,
    CardsModule,
    CoreModule,
    DashboardCardsModule,
    DashboardsModule,
    ProductsModule,
    SharedModule,
    UsersModule
  ],
  providers: [
    { provide: baseUrl, useValue: 'http://localhost:16761/' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
