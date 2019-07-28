import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { Location } from '../models';

@Injectable()
export class LocationService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Location>> {
    return this._client.get<{ locations: Array<Location> }>(`${this._baseUrl}api/locations`)
      .pipe(
        map(x => x.locations)
      );
  }

  public getById(options: { locationId: string }): Observable<Location> {
    return this._client.get<{ location: Location }>(`${this._baseUrl}api/locations/${options.locationId}`)
      .pipe(
        map(x => x.location)
      );
  }

  public remove(options: { location: Location }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.location.locationId}`, {
      headers: {
        "OperationId":"RemoveLocation"
      }
    });
  }

  public create(options: { location: Location }): Observable<{ locationId: string, version: number }> {
    return this._client.post<{ locationId: string, version: number }>(`${this._baseUrl}api/commands`, { location: options.location }, {
      headers: {
        "OperationId":"UpsertLocation"
      }
    });
  }

  public update(options: { location: Location }): Observable<{ locationId: string, version: number }> {
    return this._client.post<{ locationId: string, version: number }>(`${this._baseUrl}api/commands`, { location: options.location }, {
      headers: {
        "OperationId":"UpsertLocation"
      }
    });
  }
}
