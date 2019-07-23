import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { baseUrl, LocalStorageService, currentUserNameKey, accessTokenKey } from '@kids-toy-hive/core';

@Injectable()
export class AuthService {
  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _httpClient: HttpClient,    
    private _localStorageService: LocalStorageService
  ) {}

  public tryToLogin(options: { username: string; password: string }) {
    return this._httpClient.post<any>(`${this._baseUrl}api/users/token`, options).pipe(
      map(response => {
        this._localStorageService.put({ name: accessTokenKey, value: response.accessToken });
        this._localStorageService.put({ name: currentUserNameKey, value: options.username });
        return response.accessToken;
      })
    );
  }
}