// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';

export const CreateBookingSectionGuard: CanActivateFn = () => {
  const router = inject(Router);
  // Add your guard logic here
  return true;
};
