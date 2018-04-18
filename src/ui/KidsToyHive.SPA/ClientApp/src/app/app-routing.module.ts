import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomePageComponent } from './home/home-page.component';
import { PrivacyPageComponent } from './privacy/privacy-page.component';
import { PricingPageComponent } from './pricing/pricing-page.component';

const routes: Routes = [
  {
    path: '',
    component: HomePageComponent
  },
  {
    path: 'pricing',
    component: PricingPageComponent
  },
  {
    path: 'privacy',
    component: PrivacyPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
