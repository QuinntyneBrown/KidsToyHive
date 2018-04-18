import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToysPageComponent } from './toys-page.component';
import { ToyCategoriesGridComponent } from './toy-categories-grid.component';

const declarations = [
  ToysPageComponent,
  ToyCategoriesGridComponent
];

@NgModule({
  imports: [
    CommonModule
  ],
  declarations,
  exports: declarations
})
export class ToysModule { }
