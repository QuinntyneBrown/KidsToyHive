import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { baseUrl } from '@kids-toy-hive/core';
import { Booking } from '../models';

@Injectable()
export class BookingService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Booking>> {
    return this._client.get<{ bookings: Array<Booking> }>(`${this._baseUrl}api/bookings`)
      .pipe(
        map(x => x.bookings)
      );
  }

  public getMy(): Observable<Array<Booking>> {
    return this._client.get<{ bookings: Array<Booking> }>(`${this._baseUrl}api/bookings/my`)
      .pipe(
        map(x => x.bookings)
      );
  }

  public getById(options: { bookingId: string }): Observable<Booking> {
    return this._client.get<{ booking: Booking }>(`${this._baseUrl}api/bookings/${options.bookingId}`)
      .pipe(
        map(x => x.booking)
      );
  }

  public remove(options: { booking: Booking }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.booking.bookingId}`, {
      headers: {
        "OperationId":"RemoveBooking"
      }
    });
  }

  public create(options: { booking: Booking }): Observable<{ bookingId: string, version: number }> {
    return this._client.post<{ bookingId: string, version: number }>(`${this._baseUrl}api/commands`, { booking: options.booking }, {
      headers: {
        "OperationId":"UpsertBooking"
      }
    });
  }

  public processBookingPayment(options: { 
    number: number,
    expMonth: number,
    expYear: number,
    cvc: string,
    bookingId: string 
  }): Observable<{ bookingId: string, version: number }> {
    return this._client.post<{ bookingId: string, version: number }>(`${this._baseUrl}api/commands`, options, {
      headers: {
        "OperationId":"ProcessBookingPayment"
      }
    });
  }

  public update(options: { booking: Booking }): Observable<{ bookingId: string }> {
    return this._client.post<{ bookingId: string }>(`${this._baseUrl}api/commands`, { booking: options.booking }, {
      headers: {
        "OperationId":"UpsertBooking"
      }
    });
  }
}
