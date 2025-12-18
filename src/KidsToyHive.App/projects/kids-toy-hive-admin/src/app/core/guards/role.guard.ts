// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { inject } from '@angular/core';
import { CanActivateFn, Router, ActivatedRouteSnapshot } from '@angular/router';
import { PermissionService } from '../services/permission.service';
import { Role } from '../enums/role.enum';

/**
 * Guard that checks if user has required role(s) before allowing route access
 * Usage in routes: canActivate: [roleGuard], data: { roles: [Role.Admin] }
 */
export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const permissionService = inject(PermissionService);
  const router = inject(Router);

  const requiredRoles = route.data['roles'] as Role[];
  
  if (!requiredRoles || requiredRoles.length === 0) {
    return true;
  }

  const hasRequiredRole = permissionService.hasAnyRole(requiredRoles);

  if (hasRequiredRole) {
    return true;
  }

  // User doesn't have required role, redirect to unauthorized page or dashboard
  return router.createUrlTree(['/unauthorized']);
};
