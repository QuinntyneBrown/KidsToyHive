import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnonymousMasterPageComponent } from './anonymous-master-page.component';
import { AuthGuard } from './core/auth.guard';
import { MasterPageComponent } from './master-page.component';
import { LoginComponent } from './users/login.component';
import { DashboardPageComponent } from './dashboards/dashboard-page.component';
import { BrandsPageComponent } from './brands/brands-page.component';
import { ProductPageComponent } from './products/product-page.component';

export const routes: Routes = [
  {
    path: 'login',
    component: AnonymousMasterPageComponent,
    children: [
      {
        path: '',
        component: LoginComponent
      }
    ]
  },
  {
    path: '',
    component: MasterPageComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardPageComponent,
      },
      {
        path: 'products',
        component: ProductPageComponent,
      },
      {
        path: 'brands',
        component: BrandsPageComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
