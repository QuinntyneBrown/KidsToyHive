import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomePageComponent } from './home-page.component';
import { SharedModule } from '../shared/shared.module';
import { ToysModule } from '../toys/toys.module';

const declarations = [
  HomePageComponent
];

@NgModule({
  imports: [
    CommonModule,

    SharedModule,
    ToysModule
  ],
  declarations
})
export class HomeModule { }
