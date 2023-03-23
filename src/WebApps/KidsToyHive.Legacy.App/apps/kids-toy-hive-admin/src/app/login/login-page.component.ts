// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { AuthService, LoginRedirectService } from '@kids-toy-hive/domain';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
  selector: 'kth-login-page'
})
export class LoginPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    private _authService: AuthService,
    private _loginRedirect: LoginRedirectService
    ) { }
    
    public tryToLogin($event) {

    }
    ngOnDestroy() {
      this.onDestroy.next();	
    }
}

