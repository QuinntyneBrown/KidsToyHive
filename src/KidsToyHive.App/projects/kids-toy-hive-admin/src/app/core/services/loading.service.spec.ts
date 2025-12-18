// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { LoadingService } from './loading.service';

describe('LoadingService', () => {
  let service: LoadingService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoadingService]
    });
    service = TestBed.inject(LoadingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('show and hide', () => {
    it('should show loading when show() is called', (done) => {
      service.isLoading$.subscribe(isLoading => {
        if (isLoading) {
          expect(isLoading).toBe(true);
          done();
        }
      });
      
      service.show();
    });

    it('should hide loading when hide() is called', () => {
      service.show();
      service.hide();
      expect(service.isLoading()).toBe(false);
    });

    it('should handle multiple concurrent operations', () => {
      service.show();
      service.show();
      service.show();
      expect(service.isLoading()).toBe(true);
      
      service.hide();
      expect(service.isLoading()).toBe(true);
      
      service.hide();
      expect(service.isLoading()).toBe(true);
      
      service.hide();
      expect(service.isLoading()).toBe(false);
    });

    it('should not go below zero counter', () => {
      service.hide();
      service.hide();
      expect(service.isLoading()).toBe(false);
    });
  });

  describe('reset', () => {
    it('should reset loading state immediately', () => {
      service.show();
      service.show();
      service.show();
      expect(service.isLoading()).toBe(true);
      
      service.reset();
      expect(service.isLoading()).toBe(false);
    });
  });

  describe('isLoading', () => {
    it('should return current loading state', () => {
      expect(service.isLoading()).toBe(false);
      
      service.show();
      expect(service.isLoading()).toBe(true);
      
      service.hide();
      expect(service.isLoading()).toBe(false);
    });
  });
});
