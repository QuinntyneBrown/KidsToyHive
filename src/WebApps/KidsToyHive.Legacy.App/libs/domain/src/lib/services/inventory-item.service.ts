// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { InventoryItem } from '../models';

@Injectable()
export class InventoryItemService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<InventoryItem>> {
    return this._client.get<{ inventoryItems: Array<InventoryItem> }>(`${this._baseUrl}api/inventoryItems`)
      .pipe(
        map(x => x.inventoryItems)
      );
  }

  public getById(options: { inventoryItemId: string }): Observable<InventoryItem> {
    return this._client.get<{ inventoryItem: InventoryItem }>(`${this._baseUrl}api/inventoryItems/${options.inventoryItemId}`)
      .pipe(
        map(x => x.inventoryItem)
      );
  }

  public remove(options: { inventoryItem: InventoryItem }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.inventoryItem.inventoryItemId}`, {
      headers: {
        "OperationId":"RemoveInventoryItem"
      }
    });
  }

  public create(options: { inventoryItem: InventoryItem }): Observable<{ inventoryItemId: string }> {
    return this._client.post<{ inventoryItemId: string }>(`${this._baseUrl}api/commands`, { inventoryItem: options.inventoryItem }, {
      headers: {
        "OperationId":"UpsertInventoryItem"
      }
    });
  }

  public update(options: { inventoryItem: InventoryItem }): Observable<{ inventoryItemId: string }> {
    return this._client.post<{ inventoryItemId: string }>(`${this._baseUrl}api/commands`, { inventoryItem: options.inventoryItem }, {
      headers: {
        "OperationId":"UpsertInventoryItem"
      }
    });
  }
}

