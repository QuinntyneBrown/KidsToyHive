import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { DashboardCard } from "../models/dashboard-card.model";

@Injectable()
export class DashboardCardService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<DashboardCard>> {
    return this._client.get<{ dashboardCards: Array<DashboardCard> }>(`${this._baseUrl}/api/dashboardCards`)
      .pipe(
        map(x => x.dashboardCards)
      );
  }

  public getById(options: { dashboardCardId: number }): Observable<DashboardCard> {
    return this._client.get<{ dashboardCard: DashboardCard }>(`${this._baseUrl}/api/dashboardCards/${options.dashboardCardId}`)
      .pipe(
        map(x => x.dashboardCard)
      );
  }

  public remove(options: { dashboardCard: DashboardCard }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/dashboardCards/${options.dashboardCard.dashboardCardId}`);
  }

  public save(options: { dashboardCard: DashboardCard }): Observable<{ dashboardCardId: number }> {
    return this._client.post<{ dashboardCardId: number }>(`${this._baseUrl}/api/dashboardCards`, { dashboardCard: options.dashboardCard });
  }
}
