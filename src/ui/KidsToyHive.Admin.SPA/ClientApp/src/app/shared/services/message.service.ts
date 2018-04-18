import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Message } from "../models/message.model";

@Injectable()
export class MessageService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Message>> {
    return this._client.get<{ messages: Array<Message> }>(`${this._baseUrl}/api/messages`)
      .pipe(
        map(x => x.messages)
      );
  }

  public getById(options: { messageId: number }): Observable<Message> {
    return this._client.get<{ message: Message }>(`${this._baseUrl}/api/messages/${options.messageId}`)
      .pipe(
        map(x => x.message)
      );
  }

  public remove(options: { message: Message }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/messages/${options.message.messageId}`);
  }

  public save(options: { message: Message }): Observable<{ messageId: number }> {
    return this._client.post<{ messageId: number }>(`${this._baseUrl}/api/messages`, { message: options.message });
  }
}
