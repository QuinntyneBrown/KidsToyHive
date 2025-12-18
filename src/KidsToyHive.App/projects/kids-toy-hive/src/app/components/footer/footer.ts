// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  standalone: true,
  templateUrl: './footer.html',
  styleUrls: ['./footer.scss'],
  selector: 'kth-footer'
})
export class Footer implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

