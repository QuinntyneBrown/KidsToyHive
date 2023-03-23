// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { baseUrl, ProblemDetails } from '@kids-toy-hive/core';
import { Customer } from '../models';
import { ErrorService } from './error.service';

@Injectable()
export class CustomerService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient,
    private readonly _errorService: ErrorService
  ) { }

  public get(): Observable<Array<Customer>> {
    return this._client.get<{ customers: Array<Customer> }>(`${this._baseUrl}api/customers`)
      .pipe(
        map(x => x.customers)
      );
  }

  public getById(options: { customerId: string }): Observable<Customer> {
    return this._client.get<{ customer: Customer }>(`${this._baseUrl}api/customers/${options.customerId}`)
      .pipe(
        map(x => x.customer)
      );
  }

  public remove(options: { customer: Customer }): Observable<void> {
    return this._client.post<void>(`${this._baseUrl}api/commands/${options.customer.customerId}`, {
      headers: {
        "OperationId":"RemoveCustomer"
      }
    });
  }

  public create(options: { customer: Customer, acceptedTermsAndConditions: boolean }): Observable<{ customerId: string, version: number, accessToken: string } | ProblemDetails> {
    return this._client.post<{ 
      customerId: string, 
      version: number, 
      accessToken: string
    }>(`${this._baseUrl}api/commands`, { customer: options.customer, acceptedTermsAndConditions: options.acceptedTermsAndConditions }, {
      headers: {
        "OperationId":"UpsertCustomer"
      }
    }).pipe(
      catchError(e => {        
        return this._errorService.handleHttpError(e);
      })
    )
  }

  public update(options: { customer: Customer }): Observable<{ customerId: string }> {
    return this._client.post<{ customerId: string }>(`${this._baseUrl}api/commands`, { customer: options.customer }, {
      headers: {
        "OperationId":"UpsertCustomer"
      }
    });
  }
}

