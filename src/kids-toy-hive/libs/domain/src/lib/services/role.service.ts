import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { Role } from '../models';

@Injectable()
export class RoleService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Role>> {
    return this._client.get<{ roles: Array<Role> }>(`${this._baseUrl}api/roles`)
      .pipe(
        map(x => x.roles)
      );
  }

  public getById(options: { roleId: string }): Observable<Role> {
    return this._client.get<{ role: Role }>(`${this._baseUrl}api/roles/${options.roleId}`)
      .pipe(
        map(x => x.role)
      );
  }

  public remove(options: { role: Role }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/roles/${options.role.roleId}`);
  }

  public create(options: { role: Role }): Observable<{ roleId: string }> {
    return this._client.post<{ roleId: string }>(`${this._baseUrl}api/roles`, { role: options.role });
  }

  public update(options: { role: Role }): Observable<{ roleId: string }> {
    return this._client.put<{ roleId: string }>(`${this._baseUrl}api/roles`, { role: options.role });
  }
}
