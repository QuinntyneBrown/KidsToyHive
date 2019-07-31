import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocalStorageService } from './local-storage.service';
import { OverlayRefProvider } from './overlay-ref-provider';
import { Logger } from './logger';
export * from './constants';
export * from './local-storage.service';
export * from './base-overlay.service';
export * from './overlay-ref-provider';
export * from './overlay-ref-wrapper';
export * from './logger';

@NgModule({
  providers:[
    Logger,
    LocalStorageService,
    OverlayRefProvider,
  ],
  imports: [CommonModule]
})
export class CoreModule {}
