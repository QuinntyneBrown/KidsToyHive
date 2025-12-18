// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { Router, ActivatedRouteSnapshot } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { roleGuard } from './role.guard';
import { PermissionService } from '../services/permission.service';
import { AuthService } from '../services/auth.service';
import { Role } from '../enums/role.enum';

describe('roleGuard', () => {
  let permissionService: jasmine.SpyObj<PermissionService>;
  let router: jasmine.SpyObj<Router>;
  let routeSnapshot: ActivatedRouteSnapshot;

  beforeEach(() => {
    const permissionServiceSpy = jasmine.createSpyObj('PermissionService', ['hasAnyRole']);
    const routerSpy = jasmine.createSpyObj('Router', ['createUrlTree']);
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['isAuthenticated']);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        { provide: PermissionService, useValue: permissionServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: AuthService, useValue: authServiceSpy }
      ]
    });

    permissionService = TestBed.inject(PermissionService) as jasmine.SpyObj<PermissionService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    
    routeSnapshot = {
      data: {}
    } as ActivatedRouteSnapshot;
  });

  it('should allow access when user has required role', () => {
    routeSnapshot.data = { roles: [Role.Admin] };
    permissionService.hasAnyRole.and.returnValue(true);

    const result = TestBed.runInInjectionContext(() =>
      roleGuard(routeSnapshot)
    );

    expect(result).toBe(true);
    expect(permissionService.hasAnyRole).toHaveBeenCalledWith([Role.Admin]);
  });

  it('should redirect when user lacks required role', () => {
    routeSnapshot.data = { roles: [Role.Admin] };
    permissionService.hasAnyRole.and.returnValue(false);
    const mockUrlTree = {} as any;
    router.createUrlTree.and.returnValue(mockUrlTree);

    const result = TestBed.runInInjectionContext(() =>
      roleGuard(routeSnapshot)
    );

    expect(result).toBe(mockUrlTree);
    expect(router.createUrlTree).toHaveBeenCalledWith(['/unauthorized']);
  });

  it('should allow access when no roles are required', () => {
    routeSnapshot.data = {};

    const result = TestBed.runInInjectionContext(() =>
      roleGuard(routeSnapshot)
    );

    expect(result).toBe(true);
    expect(permissionService.hasAnyRole).not.toHaveBeenCalled();
  });

  it('should allow access when roles array is empty', () => {
    routeSnapshot.data = { roles: [] };

    const result = TestBed.runInInjectionContext(() =>
      roleGuard(routeSnapshot)
    );

    expect(result).toBe(true);
    expect(permissionService.hasAnyRole).not.toHaveBeenCalled();
  });

  it('should check for any of multiple roles', () => {
    routeSnapshot.data = { roles: [Role.Admin, Role.Manager] };
    permissionService.hasAnyRole.and.returnValue(true);

    const result = TestBed.runInInjectionContext(() =>
      roleGuard(routeSnapshot)
    );

    expect(result).toBe(true);
    expect(permissionService.hasAnyRole).toHaveBeenCalledWith([Role.Admin, Role.Manager]);
  });
});
