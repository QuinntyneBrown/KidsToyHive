import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { baseUrl, LocalStorageService, currentUserNameKey, accessTokenKey, ProblemDetails } from '@kids-toy-hive/core';
import { BehaviorSubject, Subject, throwError, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    @Inject(baseUrl) private _baseUrl: string,
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
      catchError(e => this.handleHttpError(e))
    );
  }

  public handleHttpError(response: HttpErrorResponse):Observable<ProblemDetails> {    
    return of({
      type:response.error.Type,
      title: response.error.Title,
      detail:JSON.parse(response.error.Detail)
    });
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