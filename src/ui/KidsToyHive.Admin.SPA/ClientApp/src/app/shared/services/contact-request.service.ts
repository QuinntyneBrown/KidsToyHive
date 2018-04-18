import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { ContactRequest } from "../models/contact-request.model";

@Injectable()
export class ContactRequestService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<ContactRequest>> {
    return this._client.get<{ contactRequests: Array<ContactRequest> }>(`${this._baseUrl}/api/contactRequests`)
      .pipe(
        map(x => x.contactRequests)
      );
  }

  public getById(options: { contactRequestId: number }): Observable<ContactRequest> {
    return this._client.get<{ contactRequest: ContactRequest }>(`${this._baseUrl}/api/contactRequests/${options.contactRequestId}`)
      .pipe(
        map(x => x.contactRequest)
      );
  }

  public remove(options: { contactRequest: ContactRequest }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/contactRequests/${options.contactRequest.contactRequestId}`);
  }

  public save(options: { contactRequest: ContactRequest }): Observable<{ contactRequestId: number }> {
    return this._client.post<{ contactRequestId: number }>(`${this._baseUrl}/api/contactRequests`, { contactRequest: options.contactRequest });
  }
}
