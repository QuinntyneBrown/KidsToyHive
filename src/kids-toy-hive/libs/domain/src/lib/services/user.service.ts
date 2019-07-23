import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { User } from '../models';

@Injectable()
export class UserService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<User>> {
    return this._client.get<{ users: Array<User> }>(`${this._baseUrl}api/users`)
      .pipe(
        map(x => x.users)
      );
  }

  public getById(options: { userId: string }): Observable<User> {
    return this._client.get<{ user: User }>(`${this._baseUrl}api/users/${options.userId}`)
      .pipe(
        map(x => x.user)
      );
  }

  public remove(options: { user: User }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/users/${options.user.userId}`);
  }

  public create(options: { user: User }): Observable<{ userId: string }> {
    return this._client.post<{ userId: string }>(`${this._baseUrl}api/users`, { user: options.user });
  }

  public update(options: { user: User }): Observable<{ userId: string }> {
    return this._client.put<{ userId: string }>(`${this._baseUrl}api/users`, { user: options.user });
  }
}
