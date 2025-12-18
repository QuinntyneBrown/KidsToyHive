// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { AppShellComponent } from './shell/app-shell.component';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { Role } from './core/enums/role.enum';

export const routes: Routes = [
  // Public routes
  {
    path: 'login',
    component: LoginComponent,
    title: 'Login - KidsToyHive Admin'
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
    title: 'Unauthorized - KidsToyHive Admin'
  },
  
  // Protected routes with shell
  {
    path: '',
    component: AppShellComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        component: DashboardComponent,
        title: 'Dashboard - KidsToyHive Admin',
        data: { breadcrumb: 'Dashboard' }
      },
      
      // Management routes
      {
        path: 'bookings',
        title: 'Bookings - KidsToyHive Admin',
        data: { breadcrumb: 'Bookings' },
        loadComponent: () => import('./features/bookings/bookings.component').then(m => m.BookingsComponent)
      },
      {
        path: 'brands',
        title: 'Brands - KidsToyHive Admin',
        data: { breadcrumb: 'Brands' },
        loadComponent: () => import('./features/brands/brands.component').then(m => m.BrandsComponent)
      },
      {
        path: 'contacts',
        title: 'Contacts - KidsToyHive Admin',
        data: { breadcrumb: 'Contacts' },
        loadComponent: () => import('./features/contacts/contacts.component').then(m => m.ContactsComponent)
      },
      {
        path: 'customers',
        title: 'Customers - KidsToyHive Admin',
        data: { breadcrumb: 'Customers' },
        loadComponent: () => import('./features/customers/customers.component').then(m => m.CustomersComponent)
      },
      {
        path: 'drivers',
        title: 'Drivers - KidsToyHive Admin',
        data: { breadcrumb: 'Drivers' },
        loadComponent: () => import('./features/drivers/drivers.component').then(m => m.DriversComponent)
      },
      {
        path: 'locations',
        title: 'Locations - KidsToyHive Admin',
        data: { breadcrumb: 'Locations' },
        loadComponent: () => import('./features/locations/locations.component').then(m => m.LocationsComponent)
      },
      {
        path: 'warehouses',
        title: 'Warehouses - KidsToyHive Admin',
        data: { breadcrumb: 'Warehouses' },
        loadComponent: () => import('./features/warehouses/warehouses.component').then(m => m.WarehousesComponent)
      },
      
      // Product routes
      {
        path: 'products',
        title: 'Products - KidsToyHive Admin',
        data: { breadcrumb: 'Products' },
        loadComponent: () => import('./features/products/products.component').then(m => m.ProductsComponent)
      },
      {
        path: 'product-categories',
        title: 'Product Categories - KidsToyHive Admin',
        data: { breadcrumb: 'Categories' },
        loadComponent: () => import('./features/product-categories/product-categories.component').then(m => m.ProductCategoriesComponent)
      },
      {
        path: 'taxes',
        title: 'Taxes - KidsToyHive Admin',
        data: { breadcrumb: 'Taxes' },
        loadComponent: () => import('./features/taxes/taxes.component').then(m => m.TaxesComponent)
      },
      
      // Communication routes
      {
        path: 'email-templates',
        title: 'Email Templates - KidsToyHive Admin',
        data: { breadcrumb: 'Email Templates' },
        loadComponent: () => import('./features/email-templates/email-templates.component').then(m => m.EmailTemplatesComponent)
      },
      {
        path: 'surveys',
        title: 'Surveys - KidsToyHive Admin',
        data: { breadcrumb: 'Surveys' },
        loadComponent: () => import('./features/surveys/surveys.component').then(m => m.SurveysComponent)
      },
      {
        path: 'questions',
        title: 'Questions - KidsToyHive Admin',
        data: { breadcrumb: 'Questions' },
        loadComponent: () => import('./features/questions/questions.component').then(m => m.QuestionsComponent)
      },
      {
        path: 'videos',
        title: 'Videos - KidsToyHive Admin',
        data: { breadcrumb: 'Videos' },
        loadComponent: () => import('./features/videos/videos.component').then(m => m.VideosComponent)
      },
      
      // Order routes
      {
        path: 'sales-orders',
        title: 'Sales Orders - KidsToyHive Admin',
        data: { breadcrumb: 'Sales Orders' },
        loadComponent: () => import('./features/sales-orders/sales-orders.component').then(m => m.SalesOrdersComponent)
      },
      {
        path: 'card-layouts',
        title: 'Card Layouts - KidsToyHive Admin',
        data: { breadcrumb: 'Card Layouts' },
        loadComponent: () => import('./features/card-layouts/card-layouts.component').then(m => m.CardLayoutsComponent)
      },
      
      // Administration routes
      {
        path: 'tenants',
        title: 'Tenants - KidsToyHive Admin',
        data: { breadcrumb: 'Tenants' },
        loadComponent: () => import('./features/tenants/tenants.component').then(m => m.TenantsComponent)
      },
      {
        path: 'users',
        title: 'Users - KidsToyHive Admin',
        data: { breadcrumb: 'Users' },
        loadComponent: () => import('./features/users/users.component').then(m => m.UsersComponent)
      },
      {
        path: 'roles',
        title: 'Roles - KidsToyHive Admin',
        data: { breadcrumb: 'Roles' },
        loadComponent: () => import('./features/roles/roles.component').then(m => m.RolesComponent)
      },
      {
        path: 'profiles',
        title: 'Profiles - KidsToyHive Admin',
        data: { breadcrumb: 'Profiles' },
        loadComponent: () => import('./features/profiles/profiles.component').then(m => m.ProfilesComponent)
      }
    ]
  },
  
  // 404 catch-all
  {
    path: '**',
    component: NotFoundComponent,
    title: '404 - Page Not Found'
  }
];
