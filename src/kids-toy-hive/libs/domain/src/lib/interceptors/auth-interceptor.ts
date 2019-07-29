import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  HttpEvent,
  HttpInterceptor,
  HttpRequest,
  HttpHandler
} from '@angular/common/http';
import { LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private _storage: LocalStorageService) {}

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this._storage.get({ name: accessTokenKey }) || '';

    return next.handle(
      httpRequest.clone({
        headers: httpRequest.headers
          .set('Authorization', `Bearer ${token}`)
      })
    );
  }
}