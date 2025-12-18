// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { PermissionService } from './permission.service';
import { AuthService } from './auth.service';
import { Role } from '../enums/role.enum';
import { Permission } from '../enums/permission.enum';
import { User } from '../models';

describe('PermissionService', () => {
  let service: PermissionService;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let currentUserSubject: BehaviorSubject<User | null>;

  beforeEach(() => {
    currentUserSubject = new BehaviorSubject<User | null>(null);
    
    const authSpy = jasmine.createSpyObj('AuthService', ['login', 'logout'], {
      currentUser$: currentUserSubject.asObservable(),
      currentUserSubject: currentUserSubject
    });

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        PermissionService,
        { provide: AuthService, useValue: authSpy },
        { provide: Router, useValue: jasmine.createSpyObj('Router', ['navigate']) }
      ]
    });

    service = TestBed.inject(PermissionService);
    authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('hasRole', () => {
    it('should return false when user is null', () => {
      currentUserSubject.next(null);
      expect(service.hasRole(Role.Admin)).toBe(false);
    });

    it('should return true when user has the role', () => {
      const user: User = {
        id: '1',
        email: 'admin@test.com',
        name: 'Admin',
        roles: [Role.Admin]
      };
      currentUserSubject.next(user);
      expect(service.hasRole(Role.Admin)).toBe(true);
    });

    it('should return false when user does not have the role', () => {
      const user: User = {
        id: '1',
        email: 'viewer@test.com',
        name: 'Viewer',
        roles: [Role.Viewer]
      };
      currentUserSubject.next(user);
      expect(service.hasRole(Role.Admin)).toBe(false);
    });
  });

  describe('hasAnyRole', () => {
    it('should return true if user has any of the roles', () => {
      const user: User = {
        id: '1',
        email: 'manager@test.com',
        name: 'Manager',
        roles: [Role.Manager]
      };
      currentUserSubject.next(user);
      expect(service.hasAnyRole([Role.Admin, Role.Manager])).toBe(true);
    });

    it('should return false if user has none of the roles', () => {
      const user: User = {
        id: '1',
        email: 'viewer@test.com',
        name: 'Viewer',
        roles: [Role.Viewer]
      };
      currentUserSubject.next(user);
      expect(service.hasAnyRole([Role.Admin, Role.Manager])).toBe(false);
    });
  });

  describe('hasAllRoles', () => {
    it('should return true if user has all roles', () => {
      const user: User = {
        id: '1',
        email: 'superadmin@test.com',
        name: 'Super Admin',
        roles: [Role.Admin, Role.Manager]
      };
      currentUserSubject.next(user);
      expect(service.hasAllRoles([Role.Admin, Role.Manager])).toBe(true);
    });

    it('should return false if user is missing any role', () => {
      const user: User = {
        id: '1',
        email: 'admin@test.com',
        name: 'Admin',
        roles: [Role.Admin]
      };
      currentUserSubject.next(user);
      expect(service.hasAllRoles([Role.Admin, Role.Manager])).toBe(false);
    });
  });

  describe('hasPermission', () => {
    it('should return true for role-based permission', () => {
      const user: User = {
        id: '1',
        email: 'admin@test.com',
        name: 'Admin',
        roles: [Role.Admin]
      };
      currentUserSubject.next(user);
      expect(service.hasPermission(Permission.ManageUsers)).toBe(true);
    });

    it('should return true for explicit permission', () => {
      const user: User = {
        id: '1',
        email: 'viewer@test.com',
        name: 'Viewer',
        roles: [Role.Viewer],
        permissions: [Permission.ManageProducts.toString()]
      };
      currentUserSubject.next(user);
      expect(service.hasPermission(Permission.ManageProducts)).toBe(true);
    });

    it('should return false when user lacks permission', () => {
      const user: User = {
        id: '1',
        email: 'viewer@test.com',
        name: 'Viewer',
        roles: [Role.Viewer]
      };
      currentUserSubject.next(user);
      expect(service.hasPermission(Permission.ManageUsers)).toBe(false);
    });

    it('should return false when user is null', () => {
      currentUserSubject.next(null);
      expect(service.hasPermission(Permission.ViewDashboard)).toBe(false);
    });
  });

  describe('hasMinimumRole', () => {
    it('should return true for higher role level', () => {
      const user: User = {
        id: '1',
        email: 'admin@test.com',
        name: 'Admin',
        roles: [Role.Admin]
      };
      currentUserSubject.next(user);
      expect(service.hasMinimumRole(Role.Manager)).toBe(true);
    });

    it('should return true for exact role level', () => {
      const user: User = {
        id: '1',
        email: 'manager@test.com',
        name: 'Manager',
        roles: [Role.Manager]
      };
      currentUserSubject.next(user);
      expect(service.hasMinimumRole(Role.Manager)).toBe(true);
    });

    it('should return false for lower role level', () => {
      const user: User = {
        id: '1',
        email: 'viewer@test.com',
        name: 'Viewer',
        roles: [Role.Viewer]
      };
      currentUserSubject.next(user);
      expect(service.hasMinimumRole(Role.Manager)).toBe(false);
    });
  });

  describe('getUserPermissions', () => {
    it('should return all permissions for SuperAdmin', () => {
      const user: User = {
        id: '1',
        email: 'superadmin@test.com',
        name: 'Super Admin',
        roles: [Role.SuperAdmin]
      };
      currentUserSubject.next(user);
      
      const permissions = service.getUserPermissions();
      expect(permissions).toContain(Permission.DeleteRecords);
      expect(permissions).toContain(Permission.ManageSettings);
      expect(permissions.length).toBeGreaterThan(0);
    });

    it('should combine role and explicit permissions', () => {
      const user: User = {
        id: '1',
        email: 'viewer@test.com',
        name: 'Viewer',
        roles: [Role.Viewer],
        permissions: [Permission.ManageProducts.toString()]
      };
      currentUserSubject.next(user);
      
      const permissions = service.getUserPermissions();
      expect(permissions).toContain(Permission.ViewDashboard); // from role
      expect(permissions).toContain(Permission.ManageProducts); // explicit
    });

    it('should return empty array for null user', () => {
      currentUserSubject.next(null);
      expect(service.getUserPermissions()).toEqual([]);
    });
  });

  describe('observable methods', () => {
    it('hasRole$ should emit correct value', (done) => {
      const user: User = {
        id: '1',
        email: 'admin@test.com',
        name: 'Admin',
        roles: [Role.Admin]
      };
      
      service.hasRole$(Role.Admin).subscribe(hasRole => {
        expect(hasRole).toBe(true);
        done();
      });

      currentUserSubject.next(user);
    });

    it('hasPermission$ should emit correct value', (done) => {
      const user: User = {
        id: '1',
        email: 'admin@test.com',
        name: 'Admin',
        roles: [Role.Admin]
      };
      
      service.hasPermission$(Permission.ManageUsers).subscribe(hasPermission => {
        expect(hasPermission).toBe(true);
        done();
      });

      currentUserSubject.next(user);
    });
  });
});
