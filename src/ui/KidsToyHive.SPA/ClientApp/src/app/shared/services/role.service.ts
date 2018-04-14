import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Role } from "../models/role.model";

@Injectable()
export class RoleService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Role>> {
    return this._client.get<{ roles: Array<Role> }>(`${this._baseUrl}/api/roles`)
      .pipe(
        map(x => x.roles)
      );
  }

  public getById(options: { roleId: number }): Observable<Role> {
    return this._client.get<{ role: Role }>(`${this._baseUrl}/api/roles/${options.roleId}`)
      .pipe(
        map(x => x.role)
      );
  }

  public remove(options: { role: Role }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/roles/${options.role.roleId}`);
  }

  public save(options: { role: Role }): Observable<{ roleId: number }> {
    return this._client.post<{ roleId: number }>(`${this._baseUrl}/api/roles`, { role: options.role });
  }
}
