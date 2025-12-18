// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorService } from './error.service';
import { ProblemDetails } from '../problem-details';

describe('ErrorService', () => {
  let service: ErrorService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ErrorService]
    });

    service = TestBed.inject(ErrorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should have handleHttpError method', () => {
    expect(service.handleHttpError).toBeDefined();
  });

  it('should handle HTTP error and return ProblemDetails', (done) => {
    const mockError = {
      error: {
        Type: 'ValidationError',
        Title: 'Validation failed',
        Detail: 'Invalid input'
      },
      status: 400,
      statusText: 'Bad Request'
    } as HttpErrorResponse;

    service.handleHttpError(mockError).subscribe((result: ProblemDetails) => {
      expect(result.type).toBe('ValidationError');
      expect(result.title).toBe('Validation failed');
      expect(result.detail).toBe('Invalid input');
      done();
    });
  });

  it('should parse JSON detail when possible', (done) => {
    const mockError = {
      error: {
        Type: 'Error',
        Title: 'Test Error',
        Detail: '{"field":"value"}'
      },
      status: 500,
      statusText: 'Server Error'
    } as HttpErrorResponse;

    service.handleHttpError(mockError).subscribe((result: ProblemDetails) => {
      expect(result.detail).toEqual({ field: 'value' });
      done();
    });
  });
});
