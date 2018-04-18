import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { ProductCategory } from "../models/product-category.model";

@Injectable()
export class ProductCategoryService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<ProductCategory>> {
    return this._client.get<{ productCategories: Array<ProductCategory> }>(`${this._baseUrl}/api/productCategories`)
      .pipe(
        map(x => x.productCategories)
      );
  }

  public getById(options: { productCategoryId: number }): Observable<ProductCategory> {
    return this._client.get<{ productCategory: ProductCategory }>(`${this._baseUrl}/api/productCategories/${options.productCategoryId}`)
      .pipe(
        map(x => x.productCategory)
      );
  }

  public remove(options: { productCategory: ProductCategory }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/productCategories/${options.productCategory.productCategoryId}`);
  }

  public save(options: { productCategory: ProductCategory }): Observable<{ productCategoryId: number }> {
    return this._client.post<{ productCategoryId: number }>(`${this._baseUrl}/api/productCategories`, { productCategory: options.productCategory });
  }
}
