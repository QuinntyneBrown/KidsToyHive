// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '../constants';
import { User } from '../models/user.model';

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
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.user.userId}`, {
      headers: {
        "OperationId":"RemoveUser"
      }
    });
  }

  public create(options: { user: User }): Observable<{ userId: string }> {
    return this._client.post<{ userId: string }>(`${this._baseUrl}api/commands`, { user: options.user }, {
      headers: {
        "OperationId":"UpsertUser"
      }
    });
  }

  public update(options: { user: User }): Observable<{ userId: string }> {
    return this._client.post<{ userId: string }>(`${this._baseUrl}api/commands`, { user: options.user }, {
      headers: {
        "OperationId":"UpsertUser"
      }
    });
  }
}


