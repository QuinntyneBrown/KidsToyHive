import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { Address } from '../models';

@Injectable()
export class AddressService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Address>> {
    return this._client.get<{ addresses: Array<Address> }>(`${this._baseUrl}api/addresses`)
      .pipe(
        map(x => x.addresses)
      );
  }

  public getById(options: { addressId: string }): Observable<Address> {
    return this._client.get<{ address: Address }>(`${this._baseUrl}api/addresses/${options.addressId}`)
      .pipe(
        map(x => x.address)
      );
  }



  public create(options: { address: Address }): Observable<{ addressId: string }> {
    return this._client.post<{ addressId: string }>(`${this._baseUrl}api/commands`, { address: options.address }, {
      headers: {
        "OperationId":"UpsertAddress"
      }
    });
  }

  public update(options: { address: Address }): Observable<{ addressId: string }> {
    return this._client.post<{ addressId: string }>(`${this._baseUrl}api/commands`, { address: options.address }, {
      headers: {
        "OperationId":"UpsertAddress"
      }
    });
  }
}
