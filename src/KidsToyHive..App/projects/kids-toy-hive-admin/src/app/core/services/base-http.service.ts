// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

/**
 * Abstract base class for HTTP services with CRUD operations
 * @template T The entity type this service manages
 */
export abstract class BaseHttpService<T> {
  protected readonly http = inject(HttpClient);
  protected readonly baseUrl = environment.baseUrl;
  
  /**
   * The API endpoint path for this resource (e.g., 'api/products')
   */
  protected abstract readonly endpoint: string;

  /**
   * Get all entities
   */
  getAll(): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}${this.endpoint}`)
      .pipe(catchError(this.handleError));
  }

  /**
   * Get a single entity by ID
   */
  getById(id: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}${this.endpoint}/${id}`)
      .pipe(catchError(this.handleError));
  }

  /**
   * Create a new entity
   */
  create(entity: Partial<T>): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}${this.endpoint}`, entity)
      .pipe(catchError(this.handleError));
  }

  /**
   * Update an existing entity
   */
  update(id: string, entity: Partial<T>): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}${this.endpoint}/${id}`, entity)
      .pipe(catchError(this.handleError));
  }

  /**
   * Delete an entity by ID
   */
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.endpoint}/${id}`)
      .pipe(catchError(this.handleError));
  }

  /**
   * Handle HTTP errors
   */
  protected handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An error occurred';
    
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = error.error?.message || `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    
    return throwError(() => new Error(errorMessage));
  }
}
