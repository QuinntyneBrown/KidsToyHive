// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { SalesOrderDetail } from '../models';

@Injectable()
export class SalesOrderDetailService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<SalesOrderDetail>> {
    return this._client.get<{ salesOrderDetails: Array<SalesOrderDetail> }>(`${this._baseUrl}api/salesOrderDetails`)
      .pipe(
        map(x => x.salesOrderDetails)
      );
  }

  public getById(options: { salesOrderDetailId: string }): Observable<SalesOrderDetail> {
    return this._client.get<{ salesOrderDetail: SalesOrderDetail }>(`${this._baseUrl}api/salesOrderDetails/${options.salesOrderDetailId}`)
      .pipe(
        map(x => x.salesOrderDetail)
      );
  }

  public remove(options: { salesOrderDetail: SalesOrderDetail }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.salesOrderDetail.salesOrderDetailId}`, {
      headers: {
        "OperationId":"RemoveSalesOrderDetail"
      }
    });
  }

  public create(options: { salesOrderDetail: SalesOrderDetail }): Observable<{ salesOrderDetailId: string }> {
    return this._client.post<{ salesOrderDetailId: string }>(`${this._baseUrl}api/commands`, { salesOrderDetail: options.salesOrderDetail }, {
      headers: {
        "OperationId":"UpsertSalesOrderDetail"
      }
    });
  }

  public update(options: { salesOrderDetail: SalesOrderDetail }): Observable<{ salesOrderDetailId: string }> {
    return this._client.post<{ salesOrderDetailId: string }>(`${this._baseUrl}api/commands`, { salesOrderDetail: options.salesOrderDetail }, {
      headers: {
        "OperationId":"UpsertSalesOrderDetail"
      }
    });
  }
}

