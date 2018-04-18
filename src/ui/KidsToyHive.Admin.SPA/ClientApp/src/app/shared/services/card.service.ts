import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Card } from "../models/card.model";

@Injectable()
export class CardService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Card>> {
    return this._client.get<{ cards: Array<Card> }>(`${this._baseUrl}/api/cards`)
      .pipe(
        map(x => x.cards)
      );
  }

  public getById(options: { cardId: number }): Observable<Card> {
    return this._client.get<{ card: Card }>(`${this._baseUrl}/api/cards/${options.cardId}`)
      .pipe(
        map(x => x.card)
      );
  }

  public remove(options: { card: Card }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/cards/${options.card.cardId}`);
  }

  public save(options: { card: Card }): Observable<{ cardId: number }> {
    return this._client.post<{ cardId: number }>(`${this._baseUrl}/api/cards`, { card: options.card });
  }
}
