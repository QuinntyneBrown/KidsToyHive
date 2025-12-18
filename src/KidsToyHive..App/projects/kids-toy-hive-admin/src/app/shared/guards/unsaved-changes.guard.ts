// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { CanDeactivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog.component';

/**
 * Interface for components that have unsaved changes
 */
export interface CanComponentDeactivate {
  canDeactivate: () => boolean | Observable<boolean>;
}

/**
 * Guard to prevent navigation away from components with unsaved changes
 */
export const unsavedChangesGuard: CanDeactivateFn<CanComponentDeactivate> = (component) => {
  if (!component.canDeactivate) {
    return true;
  }

  const canDeactivate = component.canDeactivate();
  
  if (typeof canDeactivate === 'boolean') {
    if (canDeactivate) {
      return true;
    }

    const dialog = inject(MatDialog);
    const dialogRef = dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Unsaved Changes',
        message: 'You have unsaved changes. Are you sure you want to leave?',
        confirmText: 'Leave',
        cancelText: 'Stay',
        confirmColor: 'warn'
      }
    });

    return dialogRef.afterClosed().pipe(
      map(result => !!result)
    );
  }

  return canDeactivate;
};
