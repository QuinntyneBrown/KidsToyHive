// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { DigitalAsset } from '../models';

@Injectable()
export class DigitalAssetService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<DigitalAsset>> {
    return this._client.get<{ digitalAssets: Array<DigitalAsset> }>(`${this._baseUrl}api/digitalAssets`)
      .pipe(
        map(x => x.digitalAssets)
      );
  }

  public getById(options: { digitalAssetId: string }): Observable<DigitalAsset> {
    return this._client.get<{ digitalAsset: DigitalAsset }>(`${this._baseUrl}api/digitalAssets/${options.digitalAssetId}`)
      .pipe(
        map(x => x.digitalAsset)
      );
  }

  public remove(options: { digitalAsset: DigitalAsset }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.digitalAsset.digitalAssetId}`, {
      headers: {
        "OperationId":"RemoveDigitalAsset"
      }
    });
  }

  public create(options: { digitalAsset: DigitalAsset }): Observable<{ digitalAssetId: string }> {
    return this._client.post<{ digitalAssetId: string }>(`${this._baseUrl}api/commands`, { digitalAsset: options.digitalAsset }, {
      headers: {
        "OperationId":"UpsertDigitalAsset"
      }
    });
  }

  public update(options: { digitalAsset: DigitalAsset }): Observable<{ digitalAssetId: string }> {
    return this._client.post<{ digitalAssetId: string }>(`${this._baseUrl}api/commands`, { digitalAsset: options.digitalAsset }, {
      headers: {
        "OperationId":"UpsertDigitalAsset"
      }
    });
  }
}

