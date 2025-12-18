// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, timer } from 'rxjs';
import { map, catchError, switchMap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

export interface DashboardMetrics {
  totalBookings: number;
  activeCustomers: number;
  pendingOrders: number;
  revenue: number;
}

export interface ActivityItem {
  id: string;
  type: 'booking' | 'order' | 'customer';
  title: string;
  description: string;
  timestamp: Date;
  icon: string;
}

export interface UpcomingBooking {
  id: string;
  customerName: string;
  date: Date;
  status: string;
  totalItems: number;
}

/**
 * Service for fetching dashboard data
 */
@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.baseUrl;

  /**
   * Get dashboard metrics (with auto-refresh every 5 minutes)
   */
  getMetrics$(): Observable<DashboardMetrics> {
    return timer(0, 300000).pipe( // 0ms initial, 300000ms (5 min) interval
      switchMap(() => this.fetchMetrics())
    );
  }

  /**
   * Fetch current metrics from API
   */
  private fetchMetrics(): Observable<DashboardMetrics> {
    return this.http.get<DashboardMetrics>(`${this.baseUrl}api/dashboard/metrics`)
      .pipe(
        catchError(() => of(this.getMockMetrics()))
      );
  }

  /**
   * Get recent activity feed (last 10 items)
   */
  getRecentActivity(): Observable<ActivityItem[]> {
    return this.http.get<ActivityItem[]>(`${this.baseUrl}api/dashboard/activity?limit=10`)
      .pipe(
        map(items => items.map(item => ({
          ...item,
          timestamp: new Date(item.timestamp)
        }))),
        catchError(() => of(this.getMockActivity()))
      );
  }

  /**
   * Get upcoming bookings (next 7 days)
   */
  getUpcomingBookings(): Observable<UpcomingBooking[]> {
    return this.http.get<UpcomingBooking[]>(`${this.baseUrl}api/dashboard/upcoming-bookings`)
      .pipe(
        map(bookings => bookings.map(booking => ({
          ...booking,
          date: new Date(booking.date)
        }))),
        catchError(() => of(this.getMockUpcomingBookings()))
      );
  }

  /**
   * Mock metrics for development/offline mode
   */
  private getMockMetrics(): DashboardMetrics {
    return {
      totalBookings: 127,
      activeCustomers: 89,
      pendingOrders: 23,
      revenue: 45678.50
    };
  }

  /**
   * Mock activity data
   */
  private getMockActivity(): ActivityItem[] {
    const now = new Date();
    return [
      {
        id: '1',
        type: 'booking',
        title: 'New Booking Created',
        description: 'Booking #1234 created by John Doe',
        timestamp: new Date(now.getTime() - 1000 * 60 * 5), // 5 min ago
        icon: 'event'
      },
      {
        id: '2',
        type: 'order',
        title: 'Order Completed',
        description: 'Order #5678 marked as completed',
        timestamp: new Date(now.getTime() - 1000 * 60 * 15), // 15 min ago
        icon: 'check_circle'
      },
      {
        id: '3',
        type: 'customer',
        title: 'New Customer Registered',
        description: 'Jane Smith joined as a new customer',
        timestamp: new Date(now.getTime() - 1000 * 60 * 30), // 30 min ago
        icon: 'person_add'
      }
    ];
  }

  /**
   * Mock upcoming bookings
   */
  private getMockUpcomingBookings(): UpcomingBooking[] {
    const now = new Date();
    return [
      {
        id: '1',
        customerName: 'John Doe',
        date: new Date(now.getTime() + 1000 * 60 * 60 * 24), // Tomorrow
        status: 'Confirmed',
        totalItems: 5
      },
      {
        id: '2',
        customerName: 'Jane Smith',
        date: new Date(now.getTime() + 1000 * 60 * 60 * 24 * 2), // In 2 days
        status: 'Pending',
        totalItems: 3
      },
      {
        id: '3',
        customerName: 'Bob Johnson',
        date: new Date(now.getTime() + 1000 * 60 * 60 * 24 * 5), // In 5 days
        status: 'Confirmed',
        totalItems: 8
      }
    ];
  }
}
