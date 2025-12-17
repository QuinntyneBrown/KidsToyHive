// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { Button } from '../../../components/button/button';

@Component({
  standalone: true,
  imports: [Button],
  templateUrl: './how-it-works.html',
  styleUrls: ['./how-it-works.scss'],
  selector: 'kth-how-it-works'
})
export class HowItWorks implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  
  @Output()
  public callToActionClick: EventEmitter<any> = new EventEmitter();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

