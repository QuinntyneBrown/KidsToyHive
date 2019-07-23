import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomePageComponent, ConfirmationPageComponent } from './pages';
import { CatalogPageComponent } from './pages/catalog-page';

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
    path: 'catalog',
    component: CatalogPageComponent,
    canActivate
  },  
  {
    path: 'confirmation/:orderId',
    component: ConfirmationPageComponent,
    canActivate
  },  
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { initialNavigation: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
