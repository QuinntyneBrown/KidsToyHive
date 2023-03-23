import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { SalesOrder } from '../models';

@Injectable()
export class SalesOrderService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<SalesOrder>> {
    return this._client.get<{ salesOrders: Array<SalesOrder> }>(`${this._baseUrl}api/salesOrders`)
      .pipe(
        map(x => x.salesOrders)
      );
  }

  public getById(options: { salesOrderId: string }): Observable<SalesOrder> {
    return this._client.get<{ salesOrder: SalesOrder }>(`${this._baseUrl}api/salesOrders/${options.salesOrderId}`)
      .pipe(
        map(x => x.salesOrder)
      );
  }

  public remove(options: { salesOrder: SalesOrder }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.salesOrder.salesOrderId}`, {
      headers: {
        "OperationId":"RemoveSalesOrder"
      }
    });
  }

  public create(options: { salesOrder: SalesOrder }): Observable<{ salesOrderId: string }> {
    return this._client.post<{ salesOrderId: string }>(`${this._baseUrl}api/commands`, { salesOrder: options.salesOrder }, {
      headers: {
        "OperationId":"UpsertSalesOrder"
      }
    });
  }

  public update(options: { salesOrder: SalesOrder }): Observable<{ salesOrderId: string }> {
    return this._client.post<{ salesOrderId: string }>(`${this._baseUrl}api/commands`, { salesOrder: options.salesOrder }, {
      headers: {
        "OperationId":"UpsertSalesOrder"
      }
    });
  }
}
