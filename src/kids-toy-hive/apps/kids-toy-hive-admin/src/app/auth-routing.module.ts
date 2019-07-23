import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginPageComponent } from './login';
import { DashboardPageComponent } from './dashboards';
import { AuthGuard } from '@kids-toy-hive/domain';

const canActivate = [
    AuthGuard
];

const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },  
  {
    path: 'dashboard',
    component: DashboardPageComponent,
    canActivate
  },
  {
    path: 'login',
    component: LoginPageComponent
  },  
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { initialNavigation: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule {}