import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CoreModule } from '@kids-toy-hive/core';
import { DomainModule, AuthGuard } from '@kids-toy-hive/domain';
import { SharedModule } from '@kids-toy-hive/shared';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { ConfirmationPageComponent } from './pages/confirmation-page';
import { OrdersPageComponent } from './pages/orders-page';
import { LoginPageComponent } from './pages/login-page';

const canActivate = [
    AuthGuard
];

const routes: Routes = [
  {
    path: '',
    redirectTo: 'orders',
    pathMatch: 'full'
  },  
  {
    path: 'orders',
    component: OrdersPageComponent,
    canActivate
  },  
  {
    path: 'confirmation',
    component: ConfirmationPageComponent,
    canActivate
  },
  {
    path: 'login',
    component: LoginPageComponent
  },  
];

@NgModule({
  declarations: [
    AppComponent,
    ConfirmationPageComponent,
    OrdersPageComponent,
    LoginPageComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes, { initialNavigation: 'enabled' }),
    
    CoreModule,
    DomainModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}