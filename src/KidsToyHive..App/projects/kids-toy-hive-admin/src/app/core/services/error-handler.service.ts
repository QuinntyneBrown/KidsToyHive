// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ErrorHandler, Injectable, inject } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

export enum ErrorCategory {
  HTTP = 'HTTP',
  Runtime = 'Runtime',
  Validation = 'Validation',
  Network = 'Network'
}

/**
 * Global error handler service that catches unhandled exceptions
 * and displays user-friendly error messages
 */
@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements ErrorHandler {
  private readonly snackBar = inject(MatSnackBar);

  /**
   * Handle errors globally
   */
  handleError(error: Error | HttpErrorResponse): void {
    const errorCategory = this.categorizeError(error);
    const errorMessage = this.getErrorMessage(error, errorCategory);

    // Log to console in development
    if (!this.isProduction()) {
      console.error('Error caught by ErrorHandlerService:', error);
    }

    // Send to monitoring service in production (placeholder)
    if (this.isProduction()) {
      this.logToMonitoringService(error, errorCategory);
    }

    // Display user-friendly message
    this.displayErrorMessage(errorMessage, errorCategory);
  }

  /**
   * Categorize the type of error
   */
  private categorizeError(error: Error | HttpErrorResponse): ErrorCategory {
    if (error instanceof HttpErrorResponse) {
      if (error.status === 0) {
        return ErrorCategory.Network;
      }
      return ErrorCategory.HTTP;
    }

    if (error.message?.includes('validation') || error.message?.includes('invalid')) {
      return ErrorCategory.Validation;
    }

    return ErrorCategory.Runtime;
  }

  /**
   * Get user-friendly error message
   */
  private getErrorMessage(error: Error | HttpErrorResponse, category: ErrorCategory): string {
    if (category === ErrorCategory.Network) {
      return 'Network error. Please check your connection and try again.';
    }

    if (error instanceof HttpErrorResponse) {
      const status = error.status;
      
      if (status === 401) {
        return 'Authentication failed. Please log in again.';
      } else if (status === 403) {
        return 'You do not have permission to perform this action.';
      } else if (status === 404) {
        return 'The requested resource was not found.';
      } else if (status >= 500) {
        return 'Server error. Please try again later.';
      } else if (error.error?.message) {
        return error.error.message;
      }
      
      return `An error occurred (${status}). Please try again.`;
    }

    if (category === ErrorCategory.Validation) {
      return error.message || 'Validation error. Please check your input.';
    }

    return 'An unexpected error occurred. Please try again.';
  }

  /**
   * Display error message to user using snackbar
   */
  private displayErrorMessage(message: string, category: ErrorCategory): void {
    const duration = category === ErrorCategory.Network ? 0 : 5000;
    const action = category === ErrorCategory.Network ? 'Retry' : 'Close';

    this.snackBar.open(message, action, {
      duration: duration,
      horizontalPosition: 'end',
      verticalPosition: 'top',
      panelClass: ['error-snackbar']
    });
  }

  /**
   * Check if running in production
   */
  private isProduction(): boolean {
    return false; // Will be replaced with actual environment check
  }

  /**
   * Log error to monitoring service (placeholder)
   */
  private logToMonitoringService(error: Error | HttpErrorResponse, category: ErrorCategory): void {
    // Placeholder for future implementation
    // Could integrate with Sentry, LogRocket, etc.
    console.log('Logging to monitoring service:', { error, category });
  }
}
