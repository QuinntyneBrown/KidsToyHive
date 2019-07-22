import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { Order } from '../models';

@Injectable()
export class OrderService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Order>> {
    return this._client.get<{ orders: Array<Order> }>(`${this._baseUrl}api/orders`)
      .pipe(
        map(x => x.orders)
      );
  }

  public getById(options: { orderId: string }): Observable<Order> {
    return this._client.get<{ order: Order }>(`${this._baseUrl}api/orders/${options.orderId}`)
      .pipe(
        map(x => x.order)
      );
  }

  public remove(options: { order: Order }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/orders/${options.order.orderId}`);
  }

  public create(options: { order: Order }): Observable<{ orderId: string }> {
    return this._client.post<{ orderId: string }>(`${this._baseUrl}api/orders`, { order: options.order });
  }

  public update(options: { order: Order }): Observable<{ orderId: string }> {
    return this._client.put<{ orderId: string }>(`${this._baseUrl}api/orders`, { order: options.order });
  }
}
