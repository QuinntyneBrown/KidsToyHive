import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { User } from "../models/user.model";

@Injectable()
export class UserService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<User>> {
    return this._client.get<{ users: Array<User> }>(`${this._baseUrl}/api/users`)
      .pipe(
        map(x => x.users)
      );
  }

  public getById(options: { userId: number }): Observable<User> {
    return this._client.get<{ user: User }>(`${this._baseUrl}/api/users/${options.userId}`)
      .pipe(
        map(x => x.user)
      );
  }

  public remove(options: { user: User }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/users/${options.user.userId}`);
  }

  public save(options: { user: User }): Observable<{ userId: number }> {
    return this._client.post<{ userId: number }>(`${this._baseUrl}/api/users`, { user: options.user });
  }
}
