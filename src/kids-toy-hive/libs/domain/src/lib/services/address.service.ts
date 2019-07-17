import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Address } from '../models';


@Injectable()
export class AddressService {
  constructor(
    @Inject('baseUrl') private _baseUrl:string,
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

  public remove(options: { address: Address }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/addresses/${options.address.addressId}`);
  }

  public create(options: { address: Address }): Observable<{ addressId: string }> {
    return this._client.post<{ addressId: string }>(`${this._baseUrl}api/addresses`, { address: options.address });
  }

  public update(options: { address: Address }): Observable<{ addressId: string }> {
    return this._client.put<{ addressId: string }>(`${this._baseUrl}api/addresses`, { address: options.address });
  }
}
