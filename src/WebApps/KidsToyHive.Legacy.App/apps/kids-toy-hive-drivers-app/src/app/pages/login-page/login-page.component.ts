// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { AuthService, LoginRedirectService } from '@kids-toy-hive/domain';
import { takeUntil } from 'rxjs/operators';

@Component({
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
  selector: 'kth-login-page'
})
export class LoginPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    private readonly _authService: AuthService,
    private readonly _loginRedirectService: LoginRedirectService
    ) { }

  public tryToLogin($event) {
    this._authService.tryToLogin({
      username: $event.value.username,
      password: $event.value.password
    })
    .pipe(takeUntil(this.onDestroy))
    .subscribe(_ => this._loginRedirectService.redirectPreLogin());
  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}

