// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { Router } from '@angular/router';
import { authInterceptor } from './auth.interceptor';
import { AuthService } from '../services/auth.service';

describe('authInterceptor', () => {
  let httpMock: HttpTestingController;
  let httpClient: HttpClient;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(() => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['getToken', 'logout']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(withInterceptors([authInterceptor])),
        provideHttpClientTesting(),
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    });

    httpMock = TestBed.inject(HttpTestingController);
    httpClient = TestBed.inject(HttpClient);
    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    localStorage.clear();
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should add Authorization header to requests', () => {
    const testToken = 'test-jwt-token';
    authService.getToken.and.returnValue(testToken);

    httpClient.get('/api/test').subscribe();

    const req = httpMock.expectOne('/api/test');
    expect(req.request.headers.has('Authorization')).toBe(true);
    expect(req.request.headers.get('Authorization')).toBe(`Bearer ${testToken}`);
    req.flush({});
  });

  it('should not add Authorization header when token is null', () => {
    authService.getToken.and.returnValue(null);

    httpClient.get('/api/test').subscribe();

    const req = httpMock.expectOne('/api/test');
    expect(req.request.headers.has('Authorization')).toBe(false);
    req.flush({});
  });

  it('should exclude /api/auth/login from token injection', () => {
    authService.getToken.and.returnValue('test-token');

    httpClient.post('/api/auth/login', {}).subscribe();

    const req = httpMock.expectOne('/api/auth/login');
    expect(req.request.headers.has('Authorization')).toBe(false);
    req.flush({});
  });

  it('should exclude /api/auth/refresh from token injection', () => {
    authService.getToken.and.returnValue('test-token');

    httpClient.post('/api/auth/refresh', {}).subscribe();

    const req = httpMock.expectOne('/api/auth/refresh');
    expect(req.request.headers.has('Authorization')).toBe(false);
    req.flush({});
  });

  it('should exclude /api/auth/logout from token injection', () => {
    authService.getToken.and.returnValue('test-token');

    httpClient.post('/api/auth/logout', {}).subscribe();

    const req = httpMock.expectOne('/api/auth/logout');
    expect(req.request.headers.has('Authorization')).toBe(false);
    req.flush({});
  });

  it('should handle 401 error and redirect to login', () => {
    authService.getToken.and.returnValue('test-token');

    httpClient.get('/api/protected').subscribe({
      error: (error) => {
        expect(error.status).toBe(401);
      }
    });

    const req = httpMock.expectOne('/api/protected');
    req.flush(
      { message: 'Unauthorized' },
      { status: 401, statusText: 'Unauthorized' }
    );

    expect(authService.logout).toHaveBeenCalled();
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
  });

  it('should not redirect to login for 401 on auth endpoints', () => {
    authService.getToken.and.returnValue(null);

    httpClient.post('/api/auth/login', {}).subscribe({
      error: () => {}
    });

    const req = httpMock.expectOne('/api/auth/login');
    req.flush(
      { message: 'Invalid credentials' },
      { status: 401, statusText: 'Unauthorized' }
    );

    expect(authService.logout).not.toHaveBeenCalled();
    expect(router.navigate).not.toHaveBeenCalled();
  });

  it('should pass through non-401 errors', () => {
    authService.getToken.and.returnValue('test-token');

    httpClient.get('/api/test').subscribe({
      error: (error) => {
        expect(error.status).toBe(500);
      }
    });

    const req = httpMock.expectOne('/api/test');
    req.flush(
      { message: 'Server Error' },
      { status: 500, statusText: 'Internal Server Error' }
    );

    expect(authService.logout).not.toHaveBeenCalled();
    expect(router.navigate).not.toHaveBeenCalled();
  });

  it('should handle multiple concurrent requests', () => {
    const testToken = 'test-jwt-token';
    authService.getToken.and.returnValue(testToken);

    httpClient.get('/api/test1').subscribe();
    httpClient.get('/api/test2').subscribe();
    httpClient.get('/api/test3').subscribe();

    const req1 = httpMock.expectOne('/api/test1');
    const req2 = httpMock.expectOne('/api/test2');
    const req3 = httpMock.expectOne('/api/test3');

    expect(req1.request.headers.get('Authorization')).toBe(`Bearer ${testToken}`);
    expect(req2.request.headers.get('Authorization')).toBe(`Bearer ${testToken}`);
    expect(req3.request.headers.get('Authorization')).toBe(`Bearer ${testToken}`);

    req1.flush({});
    req2.flush({});
    req3.flush({});
  });
});
