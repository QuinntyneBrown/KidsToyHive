import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { HowItWorksComponent } from './how-it-works.component';
import { RouterModule } from '@angular/router';
import { ButtonComponent } from './button.component';
import { HamburgerButtonComponent } from './hamburger-button.component';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations:[
    HeaderComponent,
    HowItWorksComponent,
    ButtonComponent,
    HamburgerButtonComponent
  ],
  exports:[
    HeaderComponent,
    HowItWorksComponent,
    ButtonComponent,
    HamburgerButtonComponent
  ],
  imports: [CommonModule, RouterModule, MatIconModule]
})
export class SharedModule {}
