// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { LoginRequest } from '../models';
import { Role } from '../enums/role.enum';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    const routerSpyObj = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AuthService,
        { provide: Router, useValue: routerSpyObj }
      ]
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    
    // Clear localStorage before each test
    localStorage.clear();
  });

  afterEach(() => {
    httpMock.verify();
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('login', () => {
    it('should authenticate user and store token', (done) => {
      const credentials: LoginRequest = {
        email: 'admin@example.com',
        password: 'password123'
      };

      const mockResponse = {
        accessToken: 'mock-jwt-token',
        refreshToken: 'mock-refresh-token'
      };

      service.login(credentials).subscribe({
        next: (response) => {
          expect(response).toEqual(mockResponse);
          expect(service.getToken()).toBe('mock-jwt-token');
          expect(service.getRefreshToken()).toBe('mock-refresh-token');
          done();
        }
      });

      const req = httpMock.expectOne('http://localhost:5000/api/auth/login');
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(credentials);
      req.flush(mockResponse);
    });

    it('should update authentication state on login', (done) => {
      const credentials: LoginRequest = {
        email: 'admin@example.com',
        password: 'password123'
      };

      const mockResponse = {
        accessToken: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjMiLCJlbWFpbCI6ImFkbWluQGV4YW1wbGUuY29tIiwibmFtZSI6IkFkbWluIFVzZXIiLCJyb2xlcyI6WyJBZG1pbiJdLCJleHAiOjk5OTk5OTk5OTl9.xxx'
      };

      service.isAuthenticated$.subscribe((isAuth) => {
        if (isAuth) {
          expect(isAuth).toBe(true);
          done();
        }
      });

      service.login(credentials).subscribe();

      const req = httpMock.expectOne('http://localhost:5000/api/auth/login');
      req.flush(mockResponse);
    });

    it('should handle login error', (done) => {
      const credentials: LoginRequest = {
        email: 'admin@example.com',
        password: 'wrongpassword'
      };

      service.login(credentials).subscribe({
        error: (error) => {
          expect(error).toBeTruthy();
          expect(service.isAuthenticated()).toBe(false);
          done();
        }
      });

      const req = httpMock.expectOne('http://localhost:5000/api/auth/login');
      req.flush({ message: 'Invalid credentials' }, { status: 401, statusText: 'Unauthorized' });
    });
  });

  describe('logout', () => {
    it('should clear tokens and redirect to login', () => {
      // Set up authenticated state
      localStorage.setItem('kth_admin_access_token', 'mock-token');
      localStorage.setItem('kth_admin_refresh_token', 'mock-refresh');

      service.logout();

      const req = httpMock.expectOne('http://localhost:5000/api/auth/logout');
      req.flush({});

      expect(service.getToken()).toBeNull();
      expect(service.getRefreshToken()).toBeNull();
      expect(routerSpy.navigate).toHaveBeenCalledWith(['/login']);
    });

    it('should update authentication state on logout', (done) => {
      localStorage.setItem('kth_admin_access_token', 'mock-token');

      let callCount = 0;
      service.isAuthenticated$.subscribe((isAuth) => {
        callCount++;
        if (callCount === 2) {
          expect(isAuth).toBe(false);
          done();
        }
      });

      service.logout();
      
      const req = httpMock.expectOne('http://localhost:5000/api/auth/logout');
      req.flush({});
    });
  });

  describe('isAuthenticated', () => {
    it('should return false when no token exists', () => {
      expect(service.isAuthenticated()).toBe(false);
    });

    it('should return false for expired token', () => {
      // Token with expired date
      const expiredToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjMiLCJleHAiOjE1MTYyMzkwMjJ9.xxx';
      localStorage.setItem('kth_admin_access_token', expiredToken);

      expect(service.isAuthenticated()).toBe(false);
    });

    it('should return true for valid token', () => {
      // Token with far future expiration
      const validToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjMiLCJleHAiOjk5OTk5OTk5OTl9.xxx';
      localStorage.setItem('kth_admin_access_token', validToken);

      expect(service.isAuthenticated()).toBe(true);
    });
  });

  describe('getToken', () => {
    it('should return null when no token is stored', () => {
      expect(service.getToken()).toBeNull();
    });

    it('should return stored token', () => {
      localStorage.setItem('kth_admin_access_token', 'test-token');
      expect(service.getToken()).toBe('test-token');
    });
  });

  describe('refreshToken', () => {
    it('should refresh access token', (done) => {
      localStorage.setItem('kth_admin_refresh_token', 'refresh-token');

      const mockResponse = {
        accessToken: 'new-access-token',
        refreshToken: 'new-refresh-token'
      };

      service.refreshToken().subscribe({
        next: (response) => {
          expect(response).toEqual(mockResponse);
          expect(service.getToken()).toBe('new-access-token');
          done();
        }
      });

      const req = httpMock.expectOne('http://localhost:5000/api/auth/refresh');
      expect(req.request.method).toBe('POST');
      req.flush(mockResponse);
    });

    it('should handle refresh token error and logout', (done) => {
      localStorage.setItem('kth_admin_refresh_token', 'invalid-refresh-token');

      service.refreshToken().subscribe({
        error: () => {
          expect(service.getToken()).toBeNull();
          done();
        }
      });

      const refreshReq = httpMock.expectOne('http://localhost:5000/api/auth/refresh');
      refreshReq.flush({ message: 'Invalid refresh token' }, { status: 401, statusText: 'Unauthorized' });

      const logoutReq = httpMock.expectOne('http://localhost:5000/api/auth/logout');
      logoutReq.flush({});
    });
  });

  describe('getCurrentUser', () => {
    it('should fetch current user from API', (done) => {
      const mockUser = {
        id: '123',
        email: 'admin@example.com',
        name: 'Admin User',
        roles: [Role.Admin]
      };

      service.getCurrentUser().subscribe({
        next: (user) => {
          expect(user).toEqual(mockUser);
          done();
        }
      });

      const req = httpMock.expectOne('http://localhost:5000/api/auth/me');
      expect(req.request.method).toBe('GET');
      req.flush(mockUser);
    });

    it('should handle error when fetching user', (done) => {
      service.getCurrentUser().subscribe({
        next: (user) => {
          expect(user).toBeNull();
          done();
        }
      });

      const req = httpMock.expectOne('http://localhost:5000/api/auth/me');
      req.flush({ message: 'Unauthorized' }, { status: 401, statusText: 'Unauthorized' });
    });
  });
});
