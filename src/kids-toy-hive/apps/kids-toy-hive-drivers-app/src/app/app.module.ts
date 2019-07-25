import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CoreModule, baseUrl } from '@kids-toy-hive/core';
import { DomainModule, AuthGuard } from '@kids-toy-hive/domain';
import { SharedModule } from '@kids-toy-hive/shared';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { ConfirmationPageComponent } from './pages/confirmation-page';
import { OrdersPageComponent } from './pages/orders-page';
import { LoginPageComponent, LoginComponent } from './pages/login-page';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { environment } from '../environments/environment';

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
    LoginPageComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes, { initialNavigation: 'enabled' }),
    
    CoreModule,
    DomainModule,
    SharedModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: baseUrl, useValue: environment.baseUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
