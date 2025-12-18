// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, inject } from '@angular/core';
import { Permission } from '../enums/permission.enum';
import { PermissionService } from './permission.service';

export interface NavigationItem {
  label: string;
  icon: string;
  route?: string;
  permission?: Permission;
  children?: NavigationItem[];
}

export interface NavigationGroup {
  label: string;
  items: NavigationItem[];
}

/**
 * Service to manage navigation configuration and filtering based on permissions
 */
@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  private readonly permissionService = inject(PermissionService);

  private readonly navigationConfig: NavigationGroup[] = [
    {
      label: 'Management',
      items: [
        { label: 'Bookings', icon: 'event', route: '/bookings', permission: Permission.ManageBookings },
        { label: 'Brands', icon: 'business', route: '/brands', permission: Permission.ManageBrands },
        { label: 'Contacts', icon: 'contacts', route: '/contacts', permission: Permission.ManageContacts },
        { label: 'Customers', icon: 'people', route: '/customers', permission: Permission.ManageCustomers },
        { label: 'Drivers', icon: 'local_shipping', route: '/drivers', permission: Permission.ManageDrivers },
        { label: 'Locations', icon: 'place', route: '/locations', permission: Permission.ManageLocations },
        { label: 'Warehouses', icon: 'warehouse', route: '/warehouses', permission: Permission.ManageWarehouses }
      ]
    },
    {
      label: 'Products',
      items: [
        { label: 'Products', icon: 'toys', route: '/products', permission: Permission.ManageProducts },
        { label: 'Categories', icon: 'category', route: '/product-categories', permission: Permission.ManageProducts },
        { label: 'Taxes', icon: 'account_balance', route: '/taxes', permission: Permission.ManageTaxes }
      ]
    },
    {
      label: 'Communication',
      items: [
        { label: 'Email Templates', icon: 'email', route: '/email-templates', permission: Permission.ManageEmailTemplates },
        { label: 'Surveys', icon: 'poll', route: '/surveys', permission: Permission.ManageSurveys },
        { label: 'Questions', icon: 'help', route: '/questions', permission: Permission.ManageQuestions },
        { label: 'Videos', icon: 'video_library', route: '/videos', permission: Permission.ManageVideos }
      ]
    },
    {
      label: 'Orders',
      items: [
        { label: 'Sales Orders', icon: 'shopping_cart', route: '/sales-orders', permission: Permission.ManageOrders },
        { label: 'Card Layouts', icon: 'view_module', route: '/card-layouts', permission: Permission.ManageCardLayouts }
      ]
    },
    {
      label: 'Administration',
      items: [
        { label: 'Tenants', icon: 'domain', route: '/tenants', permission: Permission.ManageTenants },
        { label: 'Users', icon: 'person', route: '/users', permission: Permission.ManageUsers },
        { label: 'Roles', icon: 'admin_panel_settings', route: '/roles', permission: Permission.ManageRoles },
        { label: 'Profiles', icon: 'account_circle', route: '/profiles', permission: Permission.ManageProfiles }
      ]
    }
  ];

  /**
   * Get all navigation groups
   */
  getNavigationGroups(): NavigationGroup[] {
    return this.navigationConfig;
  }

  /**
   * Get navigation items filtered by user permissions
   */
  getFilteredNavigationGroups(): NavigationGroup[] {
    return this.navigationConfig
      .map(group => ({
        ...group,
        items: group.items.filter(item => this.hasPermission(item))
      }))
      .filter(group => group.items.length > 0);
  }

  /**
   * Get all navigation items as flat list
   */
  getAllNavigationItems(): NavigationItem[] {
    return this.navigationConfig.flatMap(group => group.items);
  }

  /**
   * Check if user has permission for a navigation item
   */
  private hasPermission(item: NavigationItem): boolean {
    if (!item.permission) {
      return true;
    }
    return this.permissionService.hasPermission(item.permission);
  }
}
