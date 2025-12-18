// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

/**
 * Service to manage global loading state across the application
 * Uses a counter-based approach to handle multiple concurrent operations
 */
@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private loadingCounter = 0;
  private loadingSubject = new BehaviorSubject<boolean>(false);
  
  public isLoading$: Observable<boolean> = this.loadingSubject.asObservable();

  /**
   * Show loading indicator
   * Increments counter to support multiple concurrent operations
   */
  show(): void {
    this.loadingCounter++;
    if (this.loadingCounter > 0) {
      this.loadingSubject.next(true);
    }
  }

  /**
   * Hide loading indicator
   * Decrements counter and only hides when all operations complete
   */
  hide(): void {
    this.loadingCounter--;
    if (this.loadingCounter <= 0) {
      this.loadingCounter = 0;
      this.loadingSubject.next(false);
    }
  }

  /**
   * Reset loading state (clear all pending operations)
   */
  reset(): void {
    this.loadingCounter = 0;
    this.loadingSubject.next(false);
  }

  /**
   * Get current loading state value
   */
  isLoading(): boolean {
    return this.loadingSubject.value;
  }
}
