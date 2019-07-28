import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomePageComponent, ConfirmationPageComponent, OrderPageComponent, AboutPageComponent } from './pages';
import { CatalogPageComponent } from './pages/catalog-page';
import { ToysPageComponent } from './pages/toys-page';
import { TermsAndConditionsPageComponent } from './pages/terms-and-conditions-page';

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
    canActivate
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
