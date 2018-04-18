import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { ProductImage } from "../models/product-image.model";

@Injectable()
export class ProductImageService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<ProductImage>> {
    return this._client.get<{ productImages: Array<ProductImage> }>(`${this._baseUrl}/api/productImages`)
      .pipe(
        map(x => x.productImages)
      );
  }

  public getById(options: { productImageId: number }): Observable<ProductImage> {
    return this._client.get<{ productImage: ProductImage }>(`${this._baseUrl}/api/productImages/${options.productImageId}`)
      .pipe(
        map(x => x.productImage)
      );
  }

  public remove(options: { productImage: ProductImage }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/productImages/${options.productImage.productImageId}`);
  }

  public save(options: { productImage: ProductImage }): Observable<{ productImageId: number }> {
    return this._client.post<{ productImageId: number }>(`${this._baseUrl}/api/productImages`, { productImage: options.productImage });
  }
}
