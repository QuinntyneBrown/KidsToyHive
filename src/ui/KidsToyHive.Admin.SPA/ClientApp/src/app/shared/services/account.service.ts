import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Account } from "../models/account.model";

@Injectable()
export class AccountService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Account>> {
    return this._client.get<{ accounts: Array<Account> }>(`${this._baseUrl}/api/accounts`)
      .pipe(
        map(x => x.accounts)
      );
  }

  public getById(options: { accountId: number }): Observable<Account> {
    return this._client.get<{ account: Account }>(`${this._baseUrl}/api/accounts/${options.accountId}`)
      .pipe(
        map(x => x.account)
      );
  }

  public remove(options: { account: Account }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/accounts/${options.account.accountId}`);
  }

  public save(options: { account: Account }): Observable<{ accountId: number }> {
    return this._client.post<{ accountId: number }>(`${this._baseUrl}/api/accounts`, { account: options.account });
  }
}
