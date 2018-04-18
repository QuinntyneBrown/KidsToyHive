import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Dashboard } from "../models/dashboard.model";

@Injectable()
export class DashboardService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Dashboard>> {
    return this._client.get<{ dashboards: Array<Dashboard> }>(`${this._baseUrl}/api/dashboards`)
      .pipe(
        map(x => x.dashboards)
      );
  }

  public getById(options: { dashboardId: number }): Observable<Dashboard> {
    return this._client.get<{ dashboard: Dashboard }>(`${this._baseUrl}/api/dashboards/${options.dashboardId}`)
      .pipe(
        map(x => x.dashboard)
      );
  }

  public remove(options: { dashboard: Dashboard }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/dashboards/${options.dashboard.dashboardId}`);
  }

  public save(options: { dashboard: Dashboard }): Observable<{ dashboardId: number }> {
    return this._client.post<{ dashboardId: number }>(`${this._baseUrl}/api/dashboards`, { dashboard: options.dashboard });
  }
}
