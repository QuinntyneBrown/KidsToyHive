import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '@kids-toy-hive/core';
import { SharedModule } from '@kids-toy-hive/shared';
import { DomainModule } from '@kids-toy-hive/domain';
import { LoginOverlayComponent, LoginOverlay } from './login-overlay';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';

export * from './login-overlay';

@NgModule({
  providers:[
    LoginOverlay
  ],  
  declarations:[LoginOverlayComponent],
  exports:[LoginOverlayComponent],
  entryComponents:[LoginOverlayComponent],
  imports: [
    CommonModule,
    CoreModule,
    SharedModule,
    DomainModule,
    ReactiveFormsModule,
    MatIconModule
  ]
})
export class FeaturesSecurityModule {}
