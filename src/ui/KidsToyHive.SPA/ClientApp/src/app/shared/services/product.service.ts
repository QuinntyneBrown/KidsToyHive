import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

import { Product } from "../models/product.model";

@Injectable()
export class ProductService {
  constructor(
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Product>> {
    return this._client.get<{ products: Array<Product> }>(`/api/products`)
      .pipe(
        map(x => x.products)
      );
  }

  public getById(options: { productId: number }): Observable<Product> {
    return this._client.get<{ product: Product }>(`/api/products/${options.productId}`)
      .pipe(
        map(x => x.product)
      );
  }

  public remove(options: { product: Product }): Observable<void> {
    return this._client.delete<void>(`/api/products/${options.product.productId}`);
  }

  public save(options: { product: Product }): Observable<{ productId: number }> {
    return this._client.post<{ productId: number }>(`/api/products`, { product: options.product });
  }
}
