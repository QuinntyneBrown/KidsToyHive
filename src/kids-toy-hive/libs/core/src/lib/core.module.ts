import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocalStorageService } from './local-storage.service';
export * from './constants';
export * from './local-storage.service';

@NgModule({
  providers:[LocalStorageService],
  imports: [CommonModule]
})
export class CoreModule {}
