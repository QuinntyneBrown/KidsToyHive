import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Address } from "../models/address.model";

@Injectable()
export class AddressService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Address>> {
    return this._client.get<{ addresses: Array<Address> }>(`${this._baseUrl}/api/addresses`)
      .pipe(
        map(x => x.addresses)
      );
  }

  public getById(options: { addressId: number }): Observable<Address> {
    return this._client.get<{ address: Address }>(`${this._baseUrl}/api/addresses/${options.addressId}`)
      .pipe(
        map(x => x.address)
      );
  }

  public remove(options: { address: Address }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/addresses/${options.address.addressId}`);
  }

  public save(options: { address: Address }): Observable<{ addressId: number }> {
    return this._client.post<{ addressId: number }>(`${this._baseUrl}/api/addresses`, { address: options.address });
  }
}
