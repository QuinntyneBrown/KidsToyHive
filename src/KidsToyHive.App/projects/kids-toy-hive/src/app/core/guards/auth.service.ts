// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { baseUrl } from '../constants';
import { LocalStorageService } from '../local-storage.service';
import { currentUserNameKey, accessTokenKey } from '../constants';
import { ProblemDetails } from '../problem-details';
import { BehaviorSubject, Subject, throwError, Observable, of } from 'rxjs';
import { ErrorService } from '../services/error.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private readonly _errorService: ErrorService,
    private _httpClient: HttpClient,    
    private _localStorageService: LocalStorageService
  ) { }

  public isAuthenticatedChanged$:BehaviorSubject<boolean> = new BehaviorSubject(this.isAuthenticated);

  public tryToLogin(options: { username: string; password: string }) {
    return this._httpClient.post<any>(`${this._baseUrl}api/users/token`, options).pipe(
      map(response => {
        this._localStorageService.put({ name: accessTokenKey, value: response.accessToken });
        this._localStorageService.put({ name: currentUserNameKey, value: options.username });
        this.isAuthenticatedChanged$.next(true);        
        return response.accessToken;
      }),
      catchError(e => this._errorService.handleHttpError(e))
    );
  }

  public logOut() {
    localStorage.clear();
    this._localStorageService.changes$.next(null);
    this.isAuthenticatedChanged$.next(false);
  } 

  public get isAuthenticated():boolean {    
    return this._localStorageService.get({ name: accessTokenKey}) != null;
  }
}

