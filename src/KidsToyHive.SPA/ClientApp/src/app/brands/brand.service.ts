import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from "../core/constants";
import { Brand } from "./brand.model";

@Injectable()
export class BrandService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Brand>> {
    return this._client.get<{ brands: Array<Brand> }>(`${this._baseUrl}api/brands`)
      .pipe(
        map(x => x.brands)
      );
  }

  public getById(options: { brandId: string }): Observable<Brand> {
    return this._client.get<{ brand: Brand }>(`${this._baseUrl}api/brands/${options.brandId}`)
      .pipe(
        map(x => x.brand)
      );
  }

  public remove(options: { brand: Brand }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/brands/${options.brand.brandId}`);
  }

  public create(options: { brand: Brand }): Observable<{ brandId: string }> {
    return this._client.post<{ brandId: string }>(`${this._baseUrl}api/brands`, { brand: options.brand });
  }

  public update(options: { brand: Brand }): Observable<{ brandId: string }> {
    return this._client.put<{ brandId: string }>(`${this._baseUrl}api/brands`, { brand: options.brand });
  }
}
