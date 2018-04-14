import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Organization } from "../models/organization.model";

@Injectable()
export class OrganizationService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Organization>> {
    return this._client.get<{ organizations: Array<Organization> }>(`${this._baseUrl}/api/organizations`)
      .pipe(
        map(x => x.organizations)
      );
  }

  public getById(options: { organizationId: number }): Observable<Organization> {
    return this._client.get<{ organization: Organization }>(`${this._baseUrl}/api/organizations/${options.organizationId}`)
      .pipe(
        map(x => x.organization)
      );
  }

  public remove(options: { organization: Organization }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/organizations/${options.organization.organizationId}`);
  }

  public save(options: { organization: Organization }): Observable<{ organizationId: number }> {
    return this._client.post<{ organizationId: number }>(`${this._baseUrl}/api/organizations`, { organization: options.organization });
  }
}
