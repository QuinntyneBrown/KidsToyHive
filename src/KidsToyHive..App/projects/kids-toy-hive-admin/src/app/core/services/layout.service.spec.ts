// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { BreakpointObserver } from '@angular/cdk/layout';
import { LayoutService } from './layout.service';
import { of } from 'rxjs';

describe('LayoutService', () => {
  let service: LayoutService;
  let breakpointObserver: jest.Mocked<BreakpointObserver>;

  beforeEach(() => {
    const breakpointObserverMock = {
      observe: jest.fn(),
      isMatched: jest.fn()
    };

    TestBed.configureTestingModule({
      providers: [
        LayoutService,
        { provide: BreakpointObserver, useValue: breakpointObserverMock }
      ]
    });

    service = TestBed.inject(LayoutService);
    breakpointObserver = TestBed.inject(BreakpointObserver) as jest.Mocked<BreakpointObserver>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('breakpoint observables', () => {
    it('should emit mobile breakpoint changes', (done) => {
      breakpointObserver.observe.mockReturnValue(of({ matches: true, breakpoints: {} }));
      
      service.isMobile$.subscribe(isMobile => {
        expect(isMobile).toBe(true);
        done();
      });
    });

    it('should emit tablet breakpoint changes', (done) => {
      breakpointObserver.observe.mockReturnValue(of({ matches: true, breakpoints: {} }));
      
      service.isTablet$.subscribe(isTablet => {
        expect(isTablet).toBe(true);
        done();
      });
    });

    it('should emit desktop breakpoint changes', (done) => {
      breakpointObserver.observe.mockReturnValue(of({ matches: true, breakpoints: {} }));
      
      service.isDesktop$.subscribe(isDesktop => {
        expect(isDesktop).toBe(true);
        done();
      });
    });
  });

  describe('getCurrentBreakpoint', () => {
    it('should return current breakpoint state', () => {
      breakpointObserver.isMatched.mockReturnValue(true);
      
      const breakpoint = service.getCurrentBreakpoint();
      
      expect(breakpoint.isMobile).toBe(true);
      expect(breakpoint.isTablet).toBe(true);
      expect(breakpoint.isDesktop).toBe(true);
      expect(breakpoint.isHandset).toBe(true);
    });
  });

  describe('synchronous breakpoint checks', () => {
    it('should check if mobile', () => {
      breakpointObserver.isMatched.mockReturnValue(true);
      expect(service.isMobile()).toBe(true);
    });

    it('should check if tablet', () => {
      breakpointObserver.isMatched.mockReturnValue(true);
      expect(service.isTablet()).toBe(true);
    });

    it('should check if desktop', () => {
      breakpointObserver.isMatched.mockReturnValue(true);
      expect(service.isDesktop()).toBe(true);
    });

    it('should check if handset', () => {
      breakpointObserver.isMatched.mockReturnValue(true);
      expect(service.isHandset()).toBe(true);
    });
  });
});
