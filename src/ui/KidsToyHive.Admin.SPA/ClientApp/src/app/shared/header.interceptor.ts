import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptionsArgs } from "@angular/http";
import { constants } from "./constants";
import { Storage } from "./storage";
import { Observable } from "rxjs";
import { HttpClient, HttpEvent, HttpInterceptor, HttpRequest, HttpHandler } from "@angular/common/http";

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {
  constructor(private _storage: Storage) { }

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this._storage.get({ name: constants.ACCESS_TOKEN_KEY });
    
    return next.handle(httpRequest.clone({
      headers: httpRequest.headers
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type','application/json')
    }));
  }
}
