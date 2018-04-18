import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MasterPageComponent } from "./master-page.component";
import { DashboardPageComponent } from "./dashboards/dashboard-page.component";
import { AnonymousMasterPageComponent } from "./anonymous-master-page.component";
import { LoginPageComponent } from "./login/login-page.component";
import { AuthGuard } from "./shared/auth-guard";
import { SharedModule } from "./shared/shared.module";

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
  {
    path: 'dashboard',
    component: MasterPageComponent,
    children: [
      {
        path: '',
        component: DashboardPageComponent
      },
      {
        path: ':id',
        component: DashboardPageComponent
      }
    ]
  },
  {
    path: 'login',
    component: AnonymousMasterPageComponent,
    children: [
      {
        path: '',
        component: LoginPageComponent
      }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    SharedModule
  ],
  exports: [RouterModule],
  providers:[AuthGuard]
})
export class AppRouterModule { }
