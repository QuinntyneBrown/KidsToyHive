import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomePageComponent, ConfirmationPageComponent, OrderPageComponent, AboutPageComponent, CreateCustomerSectionComponent, CreateBookingSectionComponent, ProcessBookingPaymentComponent, CreateCustomerSectionGuard, CreateBookingSectionGuard, ProcessPaymentSectionGuard } from './pages';
import { ToysPageComponent } from './pages/toys-page';
import { TermsAndConditionsPageComponent } from './pages/terms-and-conditions-page';
import { DoneSectionComponent } from './pages/order-page/sections/done';
import { MyProfilePageComponent } from './pages/my-profile-page';

const canActivate = [];

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },  
  {
    path: 'home',
    component: HomePageComponent,
    canActivate
  },
  {
    path: 'toys',
    component: ToysPageComponent,
    canActivate
  },  
  {
    path: 'order',
    component: OrderPageComponent,
    canActivate,
    children: [
      {
        path: '',
        redirectTo: 'step/1',
        pathMatch: 'full'
      },
      {
        path:'step/1',
        component: CreateCustomerSectionComponent,
        canActivate:[CreateCustomerSectionGuard]
      },
      {
        path:'step/2',
        component: CreateBookingSectionComponent,
        canActivate:[CreateBookingSectionGuard]
      },
      {
        path:'step/3',
        component: ProcessBookingPaymentComponent,
        canActivate:[ProcessPaymentSectionGuard]
      },
      {
        path:'step/4',
        component: DoneSectionComponent,
        canActivate:[ProcessPaymentSectionGuard]
      }      
    ]
  },  
  {
    path: 'about',
    component: AboutPageComponent,
    canActivate
  },
  {
    path: 'legal',
    component: TermsAndConditionsPageComponent,
    canActivate
  },    
  {
    path: 'myprofile',
    component: MyProfilePageComponent,
    canActivate
  },  
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { initialNavigation: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
