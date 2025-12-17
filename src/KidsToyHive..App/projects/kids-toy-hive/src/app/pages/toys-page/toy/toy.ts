// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { Button } from '../../../components/button/button';

@Component({
  standalone: true,
  imports: [Button],
  templateUrl: './toy.html',
  styleUrls: ['./toy.scss'],
  selector: 'kth-toy'
})
export class Toy implements OnDestroy, OnInit  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Input()
  public callToActionText:string;

  @Input()
  public imageUrl:string;

  @Input()
  public title: string;

  @Output()
  public callToActionClick: EventEmitter<any> = new EventEmitter();

  @Input()
  public baseUrl:string;

  public ngOnInit() {

  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

