// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { NavigationService } from './navigation.service';
import { PermissionService } from './permission.service';
import { Permission } from '../enums/permission.enum';

describe('NavigationService', () => {
  let service: NavigationService;
  let permissionService: jest.Mocked<PermissionService>;

  beforeEach(() => {
    const permissionServiceMock = {
      hasPermission: jest.fn()
    };

    TestBed.configureTestingModule({
      providers: [
        NavigationService,
        { provide: PermissionService, useValue: permissionServiceMock }
      ]
    });

    service = TestBed.inject(NavigationService);
    permissionService = TestBed.inject(PermissionService) as jest.Mocked<PermissionService>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getNavigationGroups', () => {
    it('should return all navigation groups', () => {
      const groups = service.getNavigationGroups();
      expect(groups.length).toBeGreaterThan(0);
      expect(groups[0].label).toBeDefined();
      expect(groups[0].items).toBeDefined();
    });

    it('should have management group', () => {
      const groups = service.getNavigationGroups();
      const managementGroup = groups.find(g => g.label === 'Management');
      expect(managementGroup).toBeDefined();
      expect(managementGroup?.items.length).toBeGreaterThan(0);
    });
  });

  describe('getFilteredNavigationGroups', () => {
    it('should filter items based on permissions', () => {
      permissionService.hasPermission.mockReturnValue(false);
      
      const filteredGroups = service.getFilteredNavigationGroups();
      
      expect(filteredGroups.length).toBe(0);
    });

    it('should include items when user has permission', () => {
      permissionService.hasPermission.mockReturnValue(true);
      
      const filteredGroups = service.getFilteredNavigationGroups();
      
      expect(filteredGroups.length).toBeGreaterThan(0);
    });

    it('should remove groups with no accessible items', () => {
      permissionService.hasPermission.mockImplementation((permission: Permission) => {
        return permission === Permission.ManageProducts;
      });
      
      const filteredGroups = service.getFilteredNavigationGroups();
      
      const productsGroup = filteredGroups.find(g => g.label === 'Products');
      expect(productsGroup).toBeDefined();
      expect(productsGroup?.items.length).toBeGreaterThan(0);
    });
  });

  describe('getAllNavigationItems', () => {
    it('should return flat list of all navigation items', () => {
      const allItems = service.getAllNavigationItems();
      expect(allItems.length).toBeGreaterThan(0);
      expect(allItems[0].label).toBeDefined();
      expect(allItems[0].icon).toBeDefined();
    });
  });
});
