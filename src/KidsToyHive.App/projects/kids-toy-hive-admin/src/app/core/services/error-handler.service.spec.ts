// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ErrorHandlerService, ErrorCategory } from './error-handler.service';

describe('ErrorHandlerService', () => {
  let service: ErrorHandlerService;
  let snackBar: jest.Mocked<MatSnackBar>;

  beforeEach(() => {
    const snackBarMock = {
      open: jest.fn()
    };

    TestBed.configureTestingModule({
      providers: [
        ErrorHandlerService,
        { provide: MatSnackBar, useValue: snackBarMock }
      ]
    });

    service = TestBed.inject(ErrorHandlerService);
    snackBar = TestBed.inject(MatSnackBar) as jest.Mocked<MatSnackBar>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('handleError', () => {
    it('should display error message for runtime errors', () => {
      const error = new Error('Test error');
      
      service.handleError(error);
      
      expect(snackBar.open).toHaveBeenCalled();
    });

    it('should display network error message for status 0', () => {
      const error = new HttpErrorResponse({ status: 0, statusText: 'Unknown Error' });
      
      service.handleError(error);
      
      expect(snackBar.open).toHaveBeenCalledWith(
        expect.stringContaining('Network error'),
        'Retry',
        expect.any(Object)
      );
    });

    it('should display authentication error for 401', () => {
      const error = new HttpErrorResponse({ status: 401, statusText: 'Unauthorized' });
      
      service.handleError(error);
      
      expect(snackBar.open).toHaveBeenCalledWith(
        expect.stringContaining('Authentication'),
        'Close',
        expect.any(Object)
      );
    });

    it('should display permission error for 403', () => {
      const error = new HttpErrorResponse({ status: 403, statusText: 'Forbidden' });
      
      service.handleError(error);
      
      expect(snackBar.open).toHaveBeenCalledWith(
        expect.stringContaining('permission'),
        'Close',
        expect.any(Object)
      );
    });

    it('should display not found error for 404', () => {
      const error = new HttpErrorResponse({ status: 404, statusText: 'Not Found' });
      
      service.handleError(error);
      
      expect(snackBar.open).toHaveBeenCalledWith(
        expect.stringContaining('not found'),
        'Close',
        expect.any(Object)
      );
    });

    it('should display server error for 5xx', () => {
      const error = new HttpErrorResponse({ status: 500, statusText: 'Internal Server Error' });
      
      service.handleError(error);
      
      expect(snackBar.open).toHaveBeenCalledWith(
        expect.stringContaining('Server error'),
        'Close',
        expect.any(Object)
      );
    });
  });
});
