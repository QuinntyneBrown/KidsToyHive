// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { authGuard } from './auth.guard';
import { AuthService } from '../services/auth.service';

describe('authGuard', () => {
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;
  let routeSnapshot: ActivatedRouteSnapshot;
  let routerState: RouterStateSnapshot;

  beforeEach(() => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['isAuthenticated']);
    const routerSpy = jasmine.createSpyObj('Router', ['createUrlTree']);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    });

    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    
    routeSnapshot = {} as ActivatedRouteSnapshot;
    routerState = { url: '/dashboard' } as RouterStateSnapshot;
  });

  it('should allow access when user is authenticated', () => {
    authService.isAuthenticated.and.returnValue(true);

    const result = TestBed.runInInjectionContext(() =>
      authGuard(routeSnapshot, routerState)
    );

    expect(result).toBe(true);
    expect(authService.isAuthenticated).toHaveBeenCalled();
  });

  it('should redirect to login when user is not authenticated', () => {
    authService.isAuthenticated.and.returnValue(false);
    const mockUrlTree = {} as any;
    router.createUrlTree.and.returnValue(mockUrlTree);

    const result = TestBed.runInInjectionContext(() =>
      authGuard(routeSnapshot, routerState)
    );

    expect(result).toBe(mockUrlTree);
    expect(router.createUrlTree).toHaveBeenCalledWith(
      ['/login'],
      { queryParams: { returnUrl: '/dashboard' } }
    );
  });

  it('should preserve the attempted URL in query params', () => {
    authService.isAuthenticated.and.returnValue(false);
    const attemptedUrl = '/admin/users';
    routerState = { url: attemptedUrl } as RouterStateSnapshot;
    const mockUrlTree = {} as any;
    router.createUrlTree.and.returnValue(mockUrlTree);

    TestBed.runInInjectionContext(() =>
      authGuard(routeSnapshot, routerState)
    );

    expect(router.createUrlTree).toHaveBeenCalledWith(
      ['/login'],
      { queryParams: { returnUrl: attemptedUrl } }
    );
  });
});
