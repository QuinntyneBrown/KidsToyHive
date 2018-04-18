import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Contact } from "../models/contact.model";

@Injectable()
export class ContactService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Contact>> {
    return this._client.get<{ contacts: Array<Contact> }>(`${this._baseUrl}/api/contacts`)
      .pipe(
        map(x => x.contacts)
      );
  }

  public getById(options: { contactId: number }): Observable<Contact> {
    return this._client.get<{ contact: Contact }>(`${this._baseUrl}/api/contacts/${options.contactId}`)
      .pipe(
        map(x => x.contact)
      );
  }

  public remove(options: { contact: Contact }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/contacts/${options.contact.contactId}`);
  }

  public save(options: { contact: Contact }): Observable<{ contactId: number }> {
    return this._client.post<{ contactId: number }>(`${this._baseUrl}/api/contacts`, { contact: options.contact });
  }
}
