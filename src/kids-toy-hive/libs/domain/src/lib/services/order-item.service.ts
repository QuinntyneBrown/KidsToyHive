import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { OrderItem } from '../models';

@Injectable()
export class OrderItemService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<OrderItem>> {
    return this._client.get<{ orderItems: Array<OrderItem> }>(`${this._baseUrl}api/orderItems`)
      .pipe(
        map(x => x.orderItems)
      );
  }

  public getById(options: { orderItemId: string }): Observable<OrderItem> {
    return this._client.get<{ orderItem: OrderItem }>(`${this._baseUrl}api/orderItems/${options.orderItemId}`)
      .pipe(
        map(x => x.orderItem)
      );
  }

  public remove(options: { orderItem: OrderItem }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.orderItem.orderItemId}`, {
      headers: {
        "OperationId":"RemoveOrderItem"
      }
    });
  }

  public create(options: { orderItem: OrderItem }): Observable<{ orderItemId: string }> {
    return this._client.post<{ orderItemId: string }>(`${this._baseUrl}api/commands`, { orderItem: options.orderItem }, {
      headers: {
        "OperationId":"UpsertOrderItem"
      }
    });
  }

  public update(options: { orderItem: OrderItem }): Observable<{ orderItemId: string }> {
    return this._client.post<{ orderItemId: string }>(`${this._baseUrl}api/commands`, { orderItem: options.orderItem }, {
      headers: {
        "OperationId":"UpsertOrderItem"
      }
    });
  }
}
