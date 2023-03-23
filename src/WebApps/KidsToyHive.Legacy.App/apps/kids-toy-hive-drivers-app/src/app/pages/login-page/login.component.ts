// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  selector: 'kth-login'
})
export class LoginComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  public username: string;
  public password: string;
  public errorMessage: string;

  @Output()
  public login:EventEmitter<any> = new EventEmitter();

  public form = new FormGroup({
    username: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required])
  });

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

