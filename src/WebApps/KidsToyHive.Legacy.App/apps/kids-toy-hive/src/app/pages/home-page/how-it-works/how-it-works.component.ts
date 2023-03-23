// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './how-it-works.component.html',
  styleUrls: ['./how-it-works.component.css'],
  selector: 'kth-how-it-works'
})
export class HowItWorksComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  
  @Output()
  public callToActionClick: EventEmitter<any> = new EventEmitter();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

