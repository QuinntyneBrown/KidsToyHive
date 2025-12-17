// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { HowItWorks } from './how-it-works/how-it-works';

@Component({
  standalone: true,
  imports: [HowItWorks],
  templateUrl: './home-page.html',
  styleUrls: ['./home-page.scss'],
  selector: 'kth-home-page'
})
export class HomePage { 
  
  constructor(private _router: Router) { }
  
  public handleCallToActionClick() {
    this._router.navigateByUrl('/toys');
  }
}

