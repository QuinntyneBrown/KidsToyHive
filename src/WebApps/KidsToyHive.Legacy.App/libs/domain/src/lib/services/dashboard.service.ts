// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { Dashboard } from '../models';

@Injectable()
export class DashboardService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Dashboard>> {
    return this._client.get<{ dashboards: Array<Dashboard> }>(`${this._baseUrl}api/dashboards`)
      .pipe(
        map(x => x.dashboards)
      );
  }

  public getById(options: { dashboardId: string }): Observable<Dashboard> {
    return this._client.get<{ dashboard: Dashboard }>(`${this._baseUrl}api/dashboards/${options.dashboardId}`)
      .pipe(
        map(x => x.dashboard)
      );
  }

  public remove(options: { dashboard: Dashboard }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.dashboard.dashboardId}`, {
      headers: {
        "OperationId":"RemoveDashboard"
      }
    });
  }

  public create(options: { dashboard: Dashboard }): Observable<{ dashboardId: string }> {
    return this._client.post<{ dashboardId: string }>(`${this._baseUrl}api/commands`, { dashboard: options.dashboard }, {
      headers: {
        "OperationId":"UpsertDashboard"
      }
    });
  }

  public update(options: { dashboard: Dashboard }): Observable<{ dashboardId: string }> {
    return this._client.post<{ dashboardId: string }>(`${this._baseUrl}api/commands`, { dashboard: options.dashboard }, {
      headers: {
        "OperationId":"UpsertDashboard"
      }
    });
  }
}

