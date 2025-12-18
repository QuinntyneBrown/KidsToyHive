// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { AuthService } from '../../core/services/auth.service';
import { DashboardService, DashboardMetrics, ActivityItem, UpcomingBooking } from './dashboard.service';
import { catchError, finalize } from 'rxjs/operators';
import { of } from 'rxjs';

/**
 * Dashboard component with metrics, quick actions, and activity feed
 */
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule,
    MatListModule,
    MatProgressSpinnerModule,
    MatDividerModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly dashboardService = inject(DashboardService);
  private readonly router = inject(Router);

  currentUser$ = this.authService.currentUser$;
  
  // State signals
  metrics = signal<DashboardMetrics | null>(null);
  recentActivity = signal<ActivityItem[]>([]);
  upcomingBookings = signal<UpcomingBooking[]>([]);
  isLoadingMetrics = signal(true);
  isLoadingActivity = signal(true);
  isLoadingBookings = signal(true);
  hasError = signal(false);
  errorMessage = signal('');

  ngOnInit(): void {
    this.loadDashboardData();
  }

  /**
   * Load all dashboard data
   */
  private loadDashboardData(): void {
    this.loadMetrics();
    this.loadRecentActivity();
    this.loadUpcomingBookings();
  }

  /**
   * Load metrics with auto-refresh
   */
  private loadMetrics(): void {
    this.isLoadingMetrics.set(true);
    this.hasError.set(false);
    
    this.dashboardService.getMetrics$()
      .pipe(
        catchError(error => {
          this.hasError.set(true);
          this.errorMessage.set('Failed to load dashboard metrics');
          return of(null);
        }),
        finalize(() => this.isLoadingMetrics.set(false))
      )
      .subscribe(metrics => {
        if (metrics) {
          this.metrics.set(metrics);
        }
      });
  }

  /**
   * Load recent activity
   */
  private loadRecentActivity(): void {
    this.isLoadingActivity.set(true);
    
    this.dashboardService.getRecentActivity()
      .pipe(
        catchError(() => of([])),
        finalize(() => this.isLoadingActivity.set(false))
      )
      .subscribe(activity => {
        this.recentActivity.set(activity);
      });
  }

  /**
   * Load upcoming bookings
   */
  private loadUpcomingBookings(): void {
    this.isLoadingBookings.set(true);
    
    this.dashboardService.getUpcomingBookings()
      .pipe(
        catchError(() => of([])),
        finalize(() => this.isLoadingBookings.set(false))
      )
      .subscribe(bookings => {
        this.upcomingBookings.set(bookings);
      });
  }

  /**
   * Retry loading data on error
   */
  onRetry(): void {
    this.loadDashboardData();
  }

  /**
   * Navigate to create booking
   */
  onCreateBooking(): void {
    this.router.navigate(['/bookings']);
  }

  /**
   * Navigate to add customer
   */
  onAddCustomer(): void {
    this.router.navigate(['/customers']);
  }

  /**
   * Navigate to process order
   */
  onProcessOrder(): void {
    this.router.navigate(['/sales-orders']);
  }

  /**
   * Format currency
   */
  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(amount);
  }

  /**
   * Format relative time
   */
  formatRelativeTime(date: Date): string {
    const now = new Date();
    const diffMs = now.getTime() - date.getTime();
    const diffMins = Math.floor(diffMs / 60000);
    const diffHours = Math.floor(diffMs / 3600000);
    const diffDays = Math.floor(diffMs / 86400000);

    if (diffMins < 1) return 'Just now';
    if (diffMins < 60) return `${diffMins} min ago`;
    if (diffHours < 24) return `${diffHours} hour${diffHours > 1 ? 's' : ''} ago`;
    return `${diffDays} day${diffDays > 1 ? 's' : ''} ago`;
  }

  /**
   * Format date
   */
  formatDate(date: Date): string {
    return new Intl.DateTimeFormat('en-US', {
      month: 'short',
      day: 'numeric',
      year: 'numeric'
    }).format(date);
  }
}

