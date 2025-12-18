// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  standalone: true,
  templateUrl: './button.html',
  styleUrls: ['./button.scss'],
  selector: 'kth-button'
})
export class Button implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Input()
  public text: string;

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

