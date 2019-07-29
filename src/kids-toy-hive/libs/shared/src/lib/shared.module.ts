import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { HowItWorksComponent } from './how-it-works.component';
import { RouterModule } from '@angular/router';
import { ButtonComponent } from './button.component';

@NgModule({
  declarations:[
    HeaderComponent,
    HowItWorksComponent,
    ButtonComponent
  ],
  exports:[
    HeaderComponent,
    HowItWorksComponent,
    ButtonComponent
  ],
  imports: [CommonModule, RouterModule]
})
export class SharedModule {}
