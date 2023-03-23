// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { HtmlContent } from '../models';

@Injectable()
export class HtmlContentService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<HtmlContent>> {
    return this._client.get<{ htmlContents: Array<HtmlContent> }>(`${this._baseUrl}api/htmlContents`)
      .pipe(
        map(x => x.htmlContents)
      );
  }

  public getById(options: { htmlContentId: string }): Observable<HtmlContent> {
    return this._client.get<{ htmlContent: HtmlContent }>(`${this._baseUrl}api/htmlContents/${options.htmlContentId}`)
      .pipe(
        map(x => x.htmlContent)
      );
  }

  public getByName(options: { name: string }): Observable<HtmlContent> {    
    return this._client.get<{ htmlContent: HtmlContent }>(`${this._baseUrl}api/htmlContents/name/${options.name}`)
      .pipe(
        map(x => x.htmlContent)
      );
  }

  public remove(options: { htmlContent: HtmlContent }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.htmlContent.htmlContentId}`, {
      headers: {
        "OperationId":"RemoveHtmlContent"
      }
    });
  }

  public create(options: { htmlContent: HtmlContent }): Observable<{ htmlContentId: string, version: number }> {
    return this._client.post<{ htmlContentId: string, version: number }>(`${this._baseUrl}api/commands`, { htmlContent: options.htmlContent }, {
      headers: {
        "OperationId":"UpsertHtmlContent"
      }
    });
  }

  public update(options: { htmlContent: HtmlContent }): Observable<{ htmlContentId: string, version: number }> {
    return this._client.post<{ htmlContentId: string, version: number }>(`${this._baseUrl}api/commands`, { htmlContent: options.htmlContent }, {
      headers: {
        "OperationId":"UpsertHtmlContent"
      }
    });
  }
}

