// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { ThemeService, Theme } from './theme.service';

describe('ThemeService', () => {
  let service: ThemeService;

  beforeEach(() => {
    localStorage.clear();
    document.body.className = '';
    
    TestBed.configureTestingModule({
      providers: [ThemeService]
    });
    
    service = TestBed.inject(ThemeService);
  });

  afterEach(() => {
    localStorage.clear();
    document.body.className = '';
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('initialization', () => {
    it('should load theme from localStorage if available', () => {
      localStorage.setItem('kth_admin_theme', 'dark');
      const newService = new ThemeService();
      expect(newService.getCurrentTheme()).toBe('dark');
    });

    it('should default to light theme if no saved preference', () => {
      expect(service.getCurrentTheme()).toBe('light');
    });
  });

  describe('setTheme', () => {
    it('should set theme to dark', () => {
      service.setTheme('dark');
      expect(service.getCurrentTheme()).toBe('dark');
      expect(service.isDarkTheme()).toBe(true);
    });

    it('should set theme to light', () => {
      service.setTheme('light');
      expect(service.getCurrentTheme()).toBe('light');
      expect(service.isDarkTheme()).toBe(false);
    });

    it('should persist theme in localStorage', () => {
      service.setTheme('dark');
      expect(localStorage.getItem('kth_admin_theme')).toBe('dark');
    });

    it('should apply CSS class to body', () => {
      service.setTheme('dark');
      expect(document.body.classList.contains('dark-theme')).toBe(true);
      
      service.setTheme('light');
      expect(document.body.classList.contains('light-theme')).toBe(true);
    });
  });

  describe('toggleTheme', () => {
    it('should toggle from light to dark', () => {
      service.setTheme('light');
      service.toggleTheme();
      expect(service.getCurrentTheme()).toBe('dark');
    });

    it('should toggle from dark to light', () => {
      service.setTheme('dark');
      service.toggleTheme();
      expect(service.getCurrentTheme()).toBe('light');
    });
  });

  describe('currentTheme$ observable', () => {
    it('should emit theme changes', (done) => {
      service.currentTheme$.subscribe((theme: Theme) => {
        if (theme === 'dark') {
          expect(theme).toBe('dark');
          done();
        }
      });
      
      service.setTheme('dark');
    });
  });
});
