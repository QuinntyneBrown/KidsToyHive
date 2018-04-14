import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Booking } from "../models/booking.model";

@Injectable()
export class BookingService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Booking>> {
    return this._client.get<{ bookings: Array<Booking> }>(`${this._baseUrl}/api/bookings`)
      .pipe(
        map(x => x.bookings)
      );
  }

  public getById(options: { bookingId: number }): Observable<Booking> {
    return this._client.get<{ booking: Booking }>(`${this._baseUrl}/api/bookings/${options.bookingId}`)
      .pipe(
        map(x => x.booking)
      );
  }

  public remove(options: { booking: Booking }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/bookings/${options.booking.bookingId}`);
  }

  public save(options: { booking: Booking }): Observable<{ bookingId: number }> {
    return this._client.post<{ bookingId: number }>(`${this._baseUrl}/api/bookings`, { booking: options.booking });
  }
}
