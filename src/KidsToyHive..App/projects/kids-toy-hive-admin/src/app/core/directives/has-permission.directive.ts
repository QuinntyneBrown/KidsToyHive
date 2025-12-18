// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Directive, Input, TemplateRef, ViewContainerRef, OnInit, OnDestroy, inject } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { PermissionService } from '../services/permission.service';
import { Permission } from '../enums/permission.enum';

/**
 * Structural directive that conditionally includes a template based on user permissions
 * Usage: <div *hasPermission="Permission.ManageUsers">Content</div>
 * Usage with array: <div *hasPermission="[Permission.ManageUsers, Permission.ViewReports]">Content</div>
 */
@Directive({
  selector: '[hasPermission]',
  standalone: true
})
export class HasPermissionDirective implements OnInit, OnDestroy {
  private readonly permissionService = inject(PermissionService);
  private readonly templateRef = inject(TemplateRef<any>);
  private readonly viewContainer = inject(ViewContainerRef);
  private destroy$ = new Subject<void>();

  private permissions: Permission[] = [];

  @Input() set hasPermission(permissions: Permission | Permission[]) {
    this.permissions = Array.isArray(permissions) ? permissions : [permissions];
    this.updateView();
  }

  ngOnInit(): void {
    // Subscribe to auth changes to update view when user logs in/out
    this.permissionService.hasPermission$(this.permissions[0]).pipe(
      takeUntil(this.destroy$)
    ).subscribe(() => {
      this.updateView();
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private updateView(): void {
    const hasPermission = this.permissions.length > 0 
      ? this.permissionService.hasAnyPermission(this.permissions)
      : false;

    this.viewContainer.clear();
    if (hasPermission) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    }
  }
}
