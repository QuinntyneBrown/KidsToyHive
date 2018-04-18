import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Content } from "../models/content.model";

@Injectable()
export class ContentService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Content>> {
    return this._client.get<{ contents: Array<Content> }>(`${this._baseUrl}/api/contents`)
      .pipe(
        map(x => x.contents)
      );
  }

  public getById(options: { contentId: number }): Observable<Content> {
    return this._client.get<{ content: Content }>(`${this._baseUrl}/api/contents/${options.contentId}`)
      .pipe(
        map(x => x.content)
      );
  }

  public remove(options: { content: Content }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/contents/${options.content.contentId}`);
  }

  public save(options: { content: Content }): Observable<{ contentId: number }> {
    return this._client.post<{ contentId: number }>(`${this._baseUrl}/api/contents`, { content: options.content });
  }
}
