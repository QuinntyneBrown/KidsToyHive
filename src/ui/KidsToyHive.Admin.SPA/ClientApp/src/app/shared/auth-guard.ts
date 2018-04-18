import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Storage } from "./storage";
import { LoginRedirect } from "./login-redirect";
import { Observable } from "rxjs";
import { constants } from "./constants";
import * as jwtDecode from "jwt-decode";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private _storage: Storage,
    private _loginRedirect: LoginRedirect
  ) { }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    const token = this._storage.get({ name: constants.ACCESS_TOKEN_KEY });

    var current_time = new Date().getTime() / 1000;

    if (token && jwtDecode(token).exp > current_time)
      return Observable.of(true);

    this._loginRedirect.lastPath = state.url;
    this._loginRedirect.redirectToLogin();

    return Observable.of(false);
  }
}
