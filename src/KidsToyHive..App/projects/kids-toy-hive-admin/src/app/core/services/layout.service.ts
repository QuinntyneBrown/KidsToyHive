// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, inject } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

export interface LayoutBreakpoints {
  isMobile: boolean;
  isTablet: boolean;
  isDesktop: boolean;
  isHandset: boolean;
}

/**
 * Service to handle layout and responsive behavior using Angular Material's BreakpointObserver
 * Provides observables for different screen sizes and orientations
 */
@Injectable({
  providedIn: 'root'
})
export class LayoutService {
  private readonly breakpointObserver = inject(BreakpointObserver);

  // Mobile: < 768px
  readonly isMobile$: Observable<boolean> = this.breakpointObserver
    .observe(['(max-width: 767.98px)'])
    .pipe(
      map(result => result.matches),
      shareReplay(1)
    );

  // Tablet: 768px - 1024px
  readonly isTablet$: Observable<boolean> = this.breakpointObserver
    .observe(['(min-width: 768px) and (max-width: 1024px)'])
    .pipe(
      map(result => result.matches),
      shareReplay(1)
    );

  // Desktop: > 1024px
  readonly isDesktop$: Observable<boolean> = this.breakpointObserver
    .observe(['(min-width: 1024.01px)'])
    .pipe(
      map(result => result.matches),
      shareReplay(1)
    );

  // Handset (phones in portrait or landscape)
  readonly isHandset$: Observable<boolean> = this.breakpointObserver
    .observe([Breakpoints.Handset])
    .pipe(
      map(result => result.matches),
      shareReplay(1)
    );

  // Portrait orientation
  readonly isPortrait$: Observable<boolean> = this.breakpointObserver
    .observe(['(orientation: portrait)'])
    .pipe(
      map(result => result.matches),
      shareReplay(1)
    );

  // Landscape orientation
  readonly isLandscape$: Observable<boolean> = this.breakpointObserver
    .observe(['(orientation: landscape)'])
    .pipe(
      map(result => result.matches),
      shareReplay(1)
    );

  /**
   * Get current breakpoint state synchronously
   */
  getCurrentBreakpoint(): LayoutBreakpoints {
    return {
      isMobile: this.breakpointObserver.isMatched('(max-width: 767.98px)'),
      isTablet: this.breakpointObserver.isMatched('(min-width: 768px) and (max-width: 1024px)'),
      isDesktop: this.breakpointObserver.isMatched('(min-width: 1024.01px)'),
      isHandset: this.breakpointObserver.isMatched(Breakpoints.Handset)
    };
  }

  /**
   * Check if current viewport is handset size
   */
  isHandset(): boolean {
    return this.breakpointObserver.isMatched(Breakpoints.Handset);
  }

  /**
   * Check if current viewport is mobile size
   */
  isMobile(): boolean {
    return this.breakpointObserver.isMatched('(max-width: 767.98px)');
  }

  /**
   * Check if current viewport is tablet size
   */
  isTablet(): boolean {
    return this.breakpointObserver.isMatched('(min-width: 768px) and (max-width: 1024px)');
  }

  /**
   * Check if current viewport is desktop size
   */
  isDesktop(): boolean {
    return this.breakpointObserver.isMatched('(min-width: 1024.01px)');
  }
}
