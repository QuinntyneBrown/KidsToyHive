// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

export * from './how-it-works';
export * from './join-now';
export * from './testimonials';

@Component({
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  selector: 'kth-home-page'
})
export class HomePageComponent { 
  
  constructor(private _router: Router) { }
  
  public handleCallToActionClick() {
    this._router.navigateByUrl('/toys');
  }
}

