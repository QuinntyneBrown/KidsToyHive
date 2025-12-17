// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '../constants';
import { ShipmentSalesOrder } from '../models/shipment-sales-order.model';

@Injectable()
export class ShipmentSalesOrderService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<ShipmentSalesOrder>> {
    return this._client.get<{ shipmentSalesOrders: Array<ShipmentSalesOrder> }>(`${this._baseUrl}api/shipmentSalesOrders`)
      .pipe(
        map(x => x.shipmentSalesOrders)
      );
  }

  public getById(options: { shipmentSalesOrderId: string }): Observable<ShipmentSalesOrder> {
    return this._client.get<{ shipmentSalesOrder: ShipmentSalesOrder }>(`${this._baseUrl}api/shipmentSalesOrders/${options.shipmentSalesOrderId}`)
      .pipe(
        map(x => x.shipmentSalesOrder)
      );
  }

  public remove(options: { shipmentSalesOrder: ShipmentSalesOrder }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.shipmentSalesOrder.shipmentSalesOrderId}`, {
      headers: {
        "OperationId":"RemoveShipmentSalesOrder"
      }
    });
  }

  public create(options: { shipmentSalesOrder: ShipmentSalesOrder }): Observable<{ shipmentSalesOrderId: string }> {
    return this._client.post<{ shipmentSalesOrderId: string }>(`${this._baseUrl}api/commands`, { shipmentSalesOrder: options.shipmentSalesOrder }, {
      headers: {
        "OperationId":"UpsertShipmentSalesOrder"
      }
    });
  }

  public update(options: { shipmentSalesOrder: ShipmentSalesOrder }): Observable<{ shipmentSalesOrderId: string }> {
    return this._client.post<{ shipmentSalesOrderId: string }>(`${this._baseUrl}api/commands`, { shipmentSalesOrder: options.shipmentSalesOrder }, {
      headers: {
        "OperationId":"UpsertShipmentSalesOrder"
      }
    });
  }
}


