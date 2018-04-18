import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardPageComponent } from './dashboard-page.component';
import { MaterialModule } from '../material/material.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ],
  declarations: [DashboardPageComponent],
  exports:[DashboardPageComponent]
})
export class DashboardsModule { }
