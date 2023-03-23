// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { ProductCategory } from '../models';

@Injectable()
export class ProductCategoryService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<ProductCategory>> {
    return this._client.get<{ productCategories: Array<ProductCategory> }>(`${this._baseUrl}api/productCategories`)
      .pipe(
        map(x => x.productCategories)
      );
  }

  public getById(options: { productCategoryId: string }): Observable<ProductCategory> {
    return this._client.get<{ productCategory: ProductCategory }>(`${this._baseUrl}api/productCategories/${options.productCategoryId}`)
      .pipe(
        map(x => x.productCategory)
      );
  }

  public remove(options: { productCategory: ProductCategory }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.productCategory.productCategoryId}`, {
      headers: {
        "OperationId":"RemoveProductCategory"
      }
    });
  }

  public create(options: { productCategory: ProductCategory }): Observable<{ productCategoryId: string }> {
    return this._client.post<{ productCategoryId: string }>(`${this._baseUrl}api/commands`, { productCategory: options.productCategory }, {
      headers: {
        "OperationId":"UpsertProductCategory"
      }
    });
  }

  public update(options: { productCategory: ProductCategory }): Observable<{ productCategoryId: string }> {
    return this._client.post<{ productCategoryId: string }>(`${this._baseUrl}api/commands`, { productCategory: options.productCategory }, {
      headers: {
        "OperationId":"UpsertProductCategory"
      }
    });
  }
}

