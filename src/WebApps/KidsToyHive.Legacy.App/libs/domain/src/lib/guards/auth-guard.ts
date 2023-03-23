// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';

@Injectable()
export class LoginRedirectService {
  constructor(private _route: ActivatedRoute, private _router: Router) {}

  loginUrl = '/login';

  lastPath: string;

  defaultPath = '/';

  setLoginUrl(value) {
    this.loginUrl = value;
  }

  setDefaultUrl(value) {
    this.defaultPath = value;
  }

  public redirectToLogin() {
    this._router.navigate([this.loginUrl]);
  }

  public redirectPreLogin() {
    if (this.lastPath && this.lastPath !== this.loginUrl) {
      this._router.navigate([this.lastPath]);
      this.lastPath = '';
    } else {
      this._router.navigate([this.defaultPath]);
    }
  }
}

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private _localStorageService: LocalStorageService,
    private _loginRedirectService: LoginRedirectService,
    private readonly _router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    const token = this._localStorageService.get({ name: accessTokenKey });

    if (token) {     
      return true
    }

    this._loginRedirectService.lastPath = state.url;

    return this._router.parseUrl(this._loginRedirectService.loginUrl);
  }
}
