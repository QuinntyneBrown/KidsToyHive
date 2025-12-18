// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

/**
 * Abstract base HTTP service for CRUD operations
 * Provides standard REST API methods for entity management
 */
export abstract class BaseHttpService<T> {
  protected abstract get endpoint(): string;
  
  constructor(
    protected readonly http: HttpClient,
    protected readonly baseUrl: string
  ) {}

  /**
   * Get all entities
   */
  getAll(): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}${this.endpoint}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Get entity by ID
   */
  getById(id: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}${this.endpoint}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Create new entity
   */
  create(entity: Partial<T>): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}${this.endpoint}`, entity)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Update existing entity
   */
  update(id: string, entity: Partial<T>): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}${this.endpoint}/${id}`, entity)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Delete entity by ID
   */
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.endpoint}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
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
