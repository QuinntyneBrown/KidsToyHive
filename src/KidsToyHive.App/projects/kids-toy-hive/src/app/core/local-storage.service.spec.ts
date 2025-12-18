// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { LocalStorageService } from './local-storage.service';

describe('LocalStorageService', () => {
  let service: LocalStorageService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LocalStorageService]
    });

    service = TestBed.inject(LocalStorageService);
    localStorage.clear();
  });

  afterEach(() => {
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should put and get value from localStorage', () => {
    const key = 'testKey';
    const value = { data: 'testValue' };

    service.put({ name: key, value });
    const retrieved = service.get({ name: key });

    expect(retrieved).toEqual(value);
  });

  it('should remove value from localStorage', () => {
    const key = 'testKey';
    const value = { data: 'testValue' };

    service.put({ name: key, value });
    service.remove({ name: key });
    const retrieved = service.get({ name: key });

    expect(retrieved).toBeNull();
  });

  it('should return null for non-existent key', () => {
    const retrieved = service.get({ name: 'nonExistentKey' });
    expect(retrieved).toBeNull();
  });
});
