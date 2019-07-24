import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CoreModule } from '@kids-toy-hive/core';
import { DomainModule } from '@kids-toy-hive/domain';
import { SharedModule } from '@kids-toy-hive/shared';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { Domain } from 'domain';
import { ConfirmationPageComponent } from './pages/confirmation-page';
import { OrdersPageComponent } from './pages/orders-page';

@NgModule({
  declarations: [
    AppComponent,
    ConfirmationPageComponent,
    OrdersPageComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([], { initialNavigation: 'enabled' }),
    
    CoreModule,
    DomainModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
