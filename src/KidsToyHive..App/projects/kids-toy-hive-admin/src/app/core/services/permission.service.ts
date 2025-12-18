// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AuthService } from './auth.service';
import { Role } from '../enums/role.enum';
import { Permission } from '../enums/permission.enum';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {
  private readonly authService = inject(AuthService);

  private roleHierarchy: Record<Role, number> = {
    [Role.SuperAdmin]: 4,
    [Role.Admin]: 3,
    [Role.Manager]: 2,
    [Role.Viewer]: 1
  };

  private rolePermissions: Record<Role, Permission[]> = {
    [Role.SuperAdmin]: [
      Permission.ViewDashboard,
      Permission.ManageUsers,
      Permission.ManageProducts,
      Permission.ManageOrders,
      Permission.ViewReports,
      Permission.ManageSettings,
      Permission.DeleteRecords
    ],
    [Role.Admin]: [
      Permission.ViewDashboard,
      Permission.ManageUsers,
      Permission.ManageProducts,
      Permission.ManageOrders,
      Permission.ViewReports,
      Permission.ManageSettings
    ],
    [Role.Manager]: [
      Permission.ViewDashboard,
      Permission.ManageProducts,
      Permission.ManageOrders,
      Permission.ViewReports
    ],
    [Role.Viewer]: [
      Permission.ViewDashboard,
      Permission.ViewReports
    ]
  };

  /**
   * Check if user has a specific role
   */
  hasRole(role: Role): boolean {
    const user = this.authService.getCurrentUserValue();
    if (!user || !user.roles) {
      return false;
    }
    return user.roles.includes(role);
  }

  /**
   * Check if user has any of the specified roles
   */
  hasAnyRole(roles: Role[]): boolean {
    return roles.some(role => this.hasRole(role));
  }

  /**
   * Check if user has all of the specified roles
   */
  hasAllRoles(roles: Role[]): boolean {
    return roles.every(role => this.hasRole(role));
  }

  /**
   * Check if user has a specific permission
   */
  hasPermission(permission: Permission): boolean {
    const user = this.authService.getCurrentUserValue();
    if (!user || !user.roles) {
      return false;
    }

    // Check explicit permissions first
    if (user.permissions && user.permissions.includes(permission)) {
      return true;
    }

    // Check role-based permissions
    return user.roles.some(role => {
      const permissions = this.rolePermissions[role as Role];
      return permissions && permissions.includes(permission);
    });
  }

  /**
   * Check if user has any of the specified permissions
   */
  hasAnyPermission(permissions: Permission[]): boolean {
    return permissions.some(permission => this.hasPermission(permission));
  }

  /**
   * Check if user has all of the specified permissions
   */
  hasAllPermissions(permissions: Permission[]): boolean {
    return permissions.every(permission => this.hasPermission(permission));
  }

  /**
   * Check if user has a role at or above a minimum level
   */
  hasMinimumRole(minimumRole: Role): boolean {
    const user = this.authService.getCurrentUserValue();
    if (!user || !user.roles) {
      return false;
    }

    const minimumLevel = this.roleHierarchy[minimumRole];
    return user.roles.some(role => {
      const level = this.roleHierarchy[role as Role];
      return level >= minimumLevel;
    });
  }

  /**
   * Get all permissions for current user
   */
  getUserPermissions(): Permission[] {
    const user = this.authService.getCurrentUserValue();
    if (!user || !user.roles) {
      return [];
    }

    const permissions = new Set<Permission>();

    // Add explicit permissions
    if (user.permissions) {
      user.permissions.forEach(p => permissions.add(p as Permission));
    }

    // Add role-based permissions
    user.roles.forEach(role => {
      const rolePerms = this.rolePermissions[role as Role];
      if (rolePerms) {
        rolePerms.forEach(p => permissions.add(p));
      }
    });

    return Array.from(permissions);
  }

  /**
   * Observable version of hasRole
   */
  hasRole$(role: Role): Observable<boolean> {
    return this.authService.currentUser$.pipe(
      map(user => {
        if (!user || !user.roles) {
          return false;
        }
        return user.roles.includes(role);
      })
    );
  }

  /**
   * Observable version of hasPermission
   */
  hasPermission$(permission: Permission): Observable<boolean> {
    return this.authService.currentUser$.pipe(
      map(() => this.hasPermission(permission))
    );
  }
}
