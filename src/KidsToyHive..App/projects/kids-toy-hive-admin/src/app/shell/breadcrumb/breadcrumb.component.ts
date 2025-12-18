// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd, ActivatedRoute, RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { filter, distinctUntilChanged } from 'rxjs/operators';

export interface Breadcrumb {
  label: string;
  url: string;
  isClickable: boolean;
}

/**
 * Breadcrumb navigation component that displays current route path
 */
@Component({
  selector: 'app-breadcrumb',
  standalone: true,
  imports: [CommonModule, RouterModule, MatIconModule],
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss']
})
export class BreadcrumbComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly activatedRoute = inject(ActivatedRoute);

  breadcrumbs: Breadcrumb[] = [];

  ngOnInit(): void {
    // Build initial breadcrumbs
    this.breadcrumbs = this.buildBreadcrumbs(this.activatedRoute.root);

    // Update breadcrumbs on navigation
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.breadcrumbs = this.buildBreadcrumbs(this.activatedRoute.root);
      });
  }

  /**
   * Build breadcrumb trail from route tree
   */
  private buildBreadcrumbs(
    route: ActivatedRoute,
    url: string = '',
    breadcrumbs: Breadcrumb[] = []
  ): Breadcrumb[] {
    // Add home breadcrumb
    if (breadcrumbs.length === 0) {
      breadcrumbs.push({
        label: 'Home',
        url: '/dashboard',
        isClickable: true
      });
    }

    // Get route path segments
    const children = route.children;

    if (children.length === 0) {
      return breadcrumbs;
    }

    for (const child of children) {
      const routeURL = child.snapshot.url.map(segment => segment.path).join('/');
      if (routeURL !== '') {
        url += `/${routeURL}`;
      }

      // Get breadcrumb label from route data or path
      const label = child.snapshot.data['breadcrumb'] || this.formatLabel(routeURL);

      if (label && label !== 'Home') {
        // Check if this is the current (last) breadcrumb
        const isLast = this.isLastRoute(child);
        
        breadcrumbs.push({
          label: this.truncateLabel(label),
          url: url,
          isClickable: !isLast
        });
      }

      return this.buildBreadcrumbs(child, url, breadcrumbs);
    }

    return breadcrumbs;
  }

  /**
   * Check if route is the last in the chain
   */
  private isLastRoute(route: ActivatedRoute): boolean {
    return route.children.length === 0 || route.children.every(child => child.snapshot.url.length === 0);
  }

  /**
   * Format route path as readable label
   */
  private formatLabel(path: string): string {
    if (!path) return '';
    
    return path
      .split('-')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ');
  }

  /**
   * Truncate long breadcrumb labels at word boundaries
   */
  private truncateLabel(label: string, maxLength: number = 30): string {
    if (label.length <= maxLength) {
      return label;
    }
    
    // Find last space before maxLength
    const truncated = label.substring(0, maxLength);
    const lastSpace = truncated.lastIndexOf(' ');
    
    if (lastSpace > maxLength / 2) {
      return truncated.substring(0, lastSpace) + '...';
    }
    
    return truncated.substring(0, maxLength - 3) + '...';
  }
}
