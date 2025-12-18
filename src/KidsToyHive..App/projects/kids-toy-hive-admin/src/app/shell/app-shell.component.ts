// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnInit, inject, signal, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { MatSidenavModule, MatSidenav } from '@angular/material/sidenav';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { BreakpointObserver } from '@angular/cdk/layout';
import { LayoutService } from '../core/services/layout.service';
import { LoadingService } from '../core/services/loading.service';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';

const SIDEBAR_STATE_KEY = 'kth_admin_sidebar_state';

/**
 * Main application shell with sidebar navigation, header, and content area
 * Handles responsive layout and sidebar state persistence
 */
@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    MatSidenavModule,
    MatProgressBarModule,
    HeaderComponent,
    SidebarComponent,
    BreadcrumbComponent
  ],
  templateUrl: './app-shell.component.html',
  styleUrls: ['./app-shell.component.scss']
})
export class AppShellComponent implements OnInit {
  private readonly layoutService = inject(LayoutService);
  private readonly loadingService = inject(LoadingService);
  
  @ViewChild('sidenav') sidenav!: MatSidenav;

  // Sidebar state
  sidebarOpened = signal(true);
  sidebarMode = signal<'side' | 'over'>('side');
  
  // Loading state
  isLoading$ = this.loadingService.isLoading$;
  
  // Layout breakpoints
  isMobile$ = this.layoutService.isMobile$;

  ngOnInit(): void {
    // Load saved sidebar state
    this.loadSidebarState();
    
    // Handle responsive sidebar behavior
    this.layoutService.isMobile$.subscribe(isMobile => {
      if (isMobile) {
        this.sidebarMode.set('over');
        this.sidebarOpened.set(false);
      } else {
        this.sidebarMode.set('side');
        this.sidebarOpened.set(this.getSavedSidebarState());
      }
    });
  }

  /**
   * Toggle sidebar open/closed
   */
  toggleSidebar(): void {
    this.sidebarOpened.set(!this.sidebarOpened());
    this.saveSidebarState(this.sidebarOpened());
  }

  /**
   * Close sidebar (for mobile overlay mode)
   */
  closeSidebar(): void {
    if (this.sidebarMode() === 'over') {
      this.sidebarOpened.set(false);
    }
  }

  /**
   * Load sidebar state from localStorage
   */
  private loadSidebarState(): void {
    const saved = this.getSavedSidebarState();
    if (!this.layoutService.isMobile()) {
      this.sidebarOpened.set(saved);
    }
  }

  /**
   * Get saved sidebar state from localStorage
   */
  private getSavedSidebarState(): boolean {
    const saved = localStorage.getItem(SIDEBAR_STATE_KEY);
    return saved !== null ? saved === 'true' : true;
  }

  /**
   * Save sidebar state to localStorage
   */
  private saveSidebarState(opened: boolean): void {
    localStorage.setItem(SIDEBAR_STATE_KEY, String(opened));
  }
}
