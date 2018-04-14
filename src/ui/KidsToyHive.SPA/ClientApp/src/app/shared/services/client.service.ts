import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Client } from "../models/client.model";

@Injectable()
export class ClientService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Client>> {
    return this._client.get<{ clients: Array<Client> }>(`${this._baseUrl}/api/clients`)
      .pipe(
        map(x => x.clients)
      );
  }

  public getById(options: { clientId: number }): Observable<Client> {
    return this._client.get<{ client: Client }>(`${this._baseUrl}/api/clients/${options.clientId}`)
      .pipe(
        map(x => x.client)
      );
  }

  public remove(options: { client: Client }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/clients/${options.client.clientId}`);
  }

  public save(options: { client: Client }): Observable<{ clientId: number }> {
    return this._client.post<{ clientId: number }>(`${this._baseUrl}/api/clients`, { client: options.client });
  }
}
