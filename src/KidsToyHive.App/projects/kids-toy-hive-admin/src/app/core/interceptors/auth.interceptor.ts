// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

/**
 * HTTP Interceptor that adds Authorization header to requests
 * and handles 401 Unauthorized responses
 */
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // List of endpoints that should not have the Authorization header
  const excludedUrls = ['/api/auth/login', '/api/auth/refresh', '/api/auth/logout'];
  
  const shouldExclude = excludedUrls.some(url => req.url.includes(url));

  // Clone request and add Authorization header if not excluded
  let authReq = req;
  if (!shouldExclude) {
    const token = authService.getToken();
    if (token) {
      authReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`)
      });
    }
  }

  // Handle the request and catch 401 errors
  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && !shouldExclude) {
        // Token is invalid or expired, logout and redirect
        authService.logout();
        router.navigate(['/login']);
      }
      return throwError(() => error);
    })
  );
};
