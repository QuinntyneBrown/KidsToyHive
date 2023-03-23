// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { RouterModule } from '@angular/router';
import { ButtonComponent } from './button.component';
import { HamburgerButtonComponent } from './hamburger-button.component';
import { MatIconModule } from '@angular/material/icon';
import { FooterComponent } from './footer.component';

@NgModule({
  declarations:[
    HeaderComponent,
    ButtonComponent,
    HamburgerButtonComponent,
    FooterComponent
  ],
  exports:[
    HeaderComponent,
    ButtonComponent,
    HamburgerButtonComponent,
    FooterComponent
  ],
  imports: [CommonModule, RouterModule, MatIconModule]
})
export class SharedModule {}

