// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize, delay } from 'rxjs/operators';
import { LoadingService } from '../services/loading.service';

/**
 * HTTP Interceptor that shows/hides loading indicator for requests
 * Only shows loading indicator for requests that take > 300ms
 */
export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(LoadingService);
  
  let showLoading = false;
  
  // Show loading after 300ms delay
  const loadingTimer = setTimeout(() => {
    showLoading = true;
    loadingService.show();
  }, 300);

  return next(req).pipe(
    finalize(() => {
      clearTimeout(loadingTimer);
      if (showLoading) {
        loadingService.hide();
      }
    })
  );
};
