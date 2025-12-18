// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { LocalStorageService } from '../local-storage.service';
import { ErrorService } from './error.service';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  let localStorageService: jest.Mocked<LocalStorageService>;

  beforeEach(() => {
    const localStorageSpy = {
      get: jest.fn(),
      put: jest.fn(),
      remove: jest.fn()
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AuthService,
        ErrorService,
        { provide: 'baseUrl', useValue: 'http://localhost' },
        { provide: LocalStorageService, useValue: localStorageSpy }
      ]
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
    localStorageService = TestBed.inject(LocalStorageService) as jest.Mocked<LocalStorageService>;
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should have tryToLogin method', () => {
    expect(service.tryToLogin).toBeDefined();
  });

  it('should have logOut method', () => {
    expect(service.logOut).toBeDefined();
  });

  it('should have isAuthenticated property', () => {
    expect(service.isAuthenticated).toBeDefined();
  });
});
