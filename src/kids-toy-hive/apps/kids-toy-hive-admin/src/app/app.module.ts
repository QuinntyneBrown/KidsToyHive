import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { InventoryItemsModule } from './inventory-items';
import { LoginModule } from './login/login.module';
import { OrdersModule } from './orders';
import { UsersModule } from './users';
import { DashboardsModule } from './dashboards/dashboards.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    DashboardsModule,
    InventoryItemsModule,
    LoginModule,
    OrdersModule,
    UsersModule,
    BrowserModule,
    RouterModule.forRoot([], { initialNavigation: 'enabled' })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
