// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component';
import { DemoComponent } from './pages/demo/demo.component';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { Role } from './core/enums/role.enum';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [authGuard]
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent
  },
  {
    path: 'demo',
    component: DemoComponent,
    canActivate: [authGuard]
  },
  // Example of route with role guard
  // {
  //   path: 'admin',
  //   component: AdminComponent,
  //   canActivate: [authGuard, roleGuard],
  //   data: { roles: [Role.Admin, Role.SuperAdmin] }
  // },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];
