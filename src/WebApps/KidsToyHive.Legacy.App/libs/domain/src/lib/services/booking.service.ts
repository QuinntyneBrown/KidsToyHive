// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { baseUrl, ProblemDetails } from '@kids-toy-hive/core';
import { Booking } from '../models';
import { ErrorService } from './error.service';

@Injectable()
export class BookingService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient,
    private readonly _errorService: ErrorService
  ) { }

  public get(): Observable<Array<Booking> | ProblemDetails> {
    return this._client.get<{ bookings: Array<Booking> }>(`${this._baseUrl}api/bookings`)
      .pipe(
        map(x => x.bookings),
        catchError(e => this._errorService.handleHttpError(e))
      );
  }

  public getMy(): Observable<Array<Booking> | ProblemDetails> {
    return this._client.get<{ bookings: Array<Booking> }>(`${this._baseUrl}api/bookings/my`)
      .pipe(
        map(x => x.bookings),
        catchError(e => this._errorService.handleHttpError(e))
      );
  }

  public getById(options: { bookingId: string }): Observable<Booking | ProblemDetails> {
    return this._client.get<{ booking: Booking }>(`${this._baseUrl}api/bookings/${options.bookingId}`)
      .pipe(
        map(x => x.booking),
        catchError(e => this._errorService.handleHttpError(e))
      );
  }

  public remove(options: { booking: Booking }): Observable<void | ProblemDetails> {
    return this._client.post<void | ProblemDetails>(`${this._baseUrl}api/commands/${options.booking.bookingId}`, {
      headers: {
        "OperationId":"RemoveBooking"
      }
    }).pipe(
      catchError(e => this._errorService.handleHttpError(e))
    );
  }

  public create(options: { booking: Booking }): Observable<{ bookingId: string, version: number }  | ProblemDetails> {
    return this._client.post<{ bookingId: string, version: number }>(`${this._baseUrl}api/commands`, { booking: options.booking }, {
      headers: {
        "OperationId":"UpsertBooking"
      }
    }).pipe(
      catchError(e => this._errorService.handleHttpError(e))
    );
  }

  public processBookingPayment(options: { 
    number: number,
    expMonth: number,
    expYear: number,
    cvc: string,
    bookingId: string 
  }): Observable<{ bookingId: string, version: number } | ProblemDetails> {
    return this._client.post<{ bookingId: string, version: number }>(`${this._baseUrl}api/commands`, options, {
      headers: {
        "OperationId":"CheckoutBooking"
      }
    }).pipe(
      catchError(e => this._errorService.handleHttpError(e))
    );
  }

  public update(options: { booking: Booking }): Observable<{ bookingId: string }> {
    return this._client.post<{ bookingId: string }>(`${this._baseUrl}api/commands`, { booking: options.booking }, {
      headers: {
        "OperationId":"UpsertBooking"
      }
    });
  }
}

