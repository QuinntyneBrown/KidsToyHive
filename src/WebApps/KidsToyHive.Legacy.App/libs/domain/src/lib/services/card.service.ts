// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { Card } from '../models';

@Injectable()
export class CardService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Card>> {
    return this._client.get<{ cards: Array<Card> }>(`${this._baseUrl}api/cards`)
      .pipe(
        map(x => x.cards)
      );
  }

  public getById(options: { cardId: string }): Observable<Card> {
    return this._client.get<{ card: Card }>(`${this._baseUrl}api/cards/${options.cardId}`)
      .pipe(
        map(x => x.card)
      );
  }

  public remove(options: { card: Card }): Observable<void> {    
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.card.cardId}`,{
      headers: {
        "OperationId":"RemoveCard"
      }
    });
  }

  public create(options: { card: Card }): Observable<{ cardId: string }> {
    return this._client.post<{ cardId: string }>(`${this._baseUrl}api/commands`, { card: options.card }, {
      headers: {
        "OperationId":"UpsertCard"
      }
    });
  }

  public update(options: { card: Card }): Observable<{ cardId: string }> {
    return this._client.post<{ cardId: string }>(`${this._baseUrl}api/commands`, { card: options.card }, {
      headers: {
        "OperationId":"UpsertCard"
      }
    });
  }
}

