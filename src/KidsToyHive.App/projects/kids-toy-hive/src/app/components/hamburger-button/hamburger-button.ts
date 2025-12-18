// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';

@Component({
  standalone: true,
  imports: [MatIconModule],
  templateUrl: './hamburger-button.html',
  styleUrls: ['./hamburger-button.scss'],
  selector: 'kth-hamburger-button'
})
export class HamburgerButton implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

