import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { DashboardPageComponent } from './dashboard-page.component';
import { CoreModule } from '@kids-toy-hive/core';
import { SharedModule } from '@kids-toy-hive/shared';
import { DomainModule } from '@kids-toy-hive/domain';

const declarations = [
  DashboardPageComponent
];

const entryComponents = [

];

const providers = [

];

@NgModule({
  declarations,
  entryComponents,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    CoreModule,
    DomainModule,
    SharedModule	
  ],
  providers,
})
export class DashboardsModule { }
