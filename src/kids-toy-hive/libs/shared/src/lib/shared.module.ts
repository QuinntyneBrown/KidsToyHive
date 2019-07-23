import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { HowItWorksComponent } from './how-it-works.component';

@NgModule({
  declarations:[
    HeaderComponent,
    HowItWorksComponent
  ],
  exports:[
    HeaderComponent,
    HowItWorksComponent
  ],
  imports: [CommonModule]
})
export class SharedModule {}
