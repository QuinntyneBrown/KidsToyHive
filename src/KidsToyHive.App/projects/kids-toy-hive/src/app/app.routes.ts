// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { OrderPage } from './pages/order-page/order-page';
import { AboutPage } from './pages/about-page/about-page';
import { CreateCustomerSection } from './pages/order-page/sections/create-customer/create-customer-section';
import { CreateBookingSection } from './pages/order-page/sections/create-booking/create-booking-section';
import { ProcessBookingPayment } from './pages/order-page/sections/process-booking-payment/process-booking-payment';
import { CreateCustomerSectionGuard } from './core/guards/create-customer-section.guard';
import { CreateBookingSectionGuard } from './core/guards/create-booking-section.guard';
import { ProcessPaymentSectionGuard } from './core/guards/process-payment-section.guard';
import { ToysPage } from './pages/toys-page/toys-page';
import { TermsAndConditionsPage } from './pages/terms-and-conditions-page/terms-and-conditions-page';
import { MyProfilePage } from './pages/my-profile-page/my-profile-page';

const canActivate = [];

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },  
  {
    path: 'home',
    component: HomePage,
    canActivate
  },
  {
    path: 'toys',
    component: ToysPage,
    canActivate
  },  
  {
    path: 'order',
    component: OrderPage,
    canActivate,
    children: [
      {
        path: '',
        redirectTo: 'step/1',
        pathMatch: 'full'
      },
      {
        path:'step/1',
        component: CreateCustomerSection,
        canActivate:[CreateCustomerSectionGuard]
      },
      {
        path:'step/2',
        component: CreateBookingSection,
        canActivate:[CreateBookingSectionGuard]
      },
      {
        path:'step/3',
        component: ProcessBookingPayment,
        canActivate:[ProcessPaymentSectionGuard]
      }  
    ]
  },  
  {
    path: 'about',
    component: AboutPage,
    canActivate
  },
  {
    path: 'legal',
    component: TermsAndConditionsPage,
    canActivate
  },    
  {
    path: 'myprofile',
    component: MyProfilePage,
    canActivate
  },  
];
