// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { BaseHttpService } from './base-http.service';
import { Injectable } from '@angular/core';
import { BaseEntity } from '../models';

interface TestEntity extends BaseEntity {
  name: string;
}

@Injectable()
class TestService extends BaseHttpService<TestEntity> {
  protected readonly endpoint = 'api/test';
}

describe('BaseHttpService', () => {
  let service: TestService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TestService]
    });
    service = TestBed.inject(TestService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getAll', () => {
    it('should return all entities', () => {
      const mockEntities: TestEntity[] = [
        { id: '1', name: 'Test 1' },
        { id: '2', name: 'Test 2' }
      ];

      service.getAll().subscribe(entities => {
        expect(entities).toEqual(mockEntities);
      });

      const req = httpMock.expectOne((service as any).baseUrl + 'api/test');
      expect(req.request.method).toBe('GET');
      req.flush(mockEntities);
    });

    it('should handle errors', () => {
      service.getAll().subscribe({
        next: () => fail('should have failed'),
        error: (error) => {
          expect(error).toBeTruthy();
        }
      });

      const req = httpMock.expectOne((service as any).baseUrl + 'api/test');
      req.flush('Error', { status: 500, statusText: 'Server Error' });
    });
  });

  describe('getById', () => {
    it('should return a single entity', () => {
      const mockEntity: TestEntity = { id: '1', name: 'Test 1' };

      service.getById('1').subscribe(entity => {
        expect(entity).toEqual(mockEntity);
      });

      const req = httpMock.expectOne((service as any).baseUrl + 'api/test/1');
      expect(req.request.method).toBe('GET');
      req.flush(mockEntity);
    });
  });

  describe('create', () => {
    it('should create a new entity', () => {
      const newEntity: Partial<TestEntity> = { name: 'New Test' };
      const mockResponse: TestEntity = { id: '3', name: 'New Test' };

      service.create(newEntity).subscribe(entity => {
        expect(entity).toEqual(mockResponse);
      });

      const req = httpMock.expectOne((service as any).baseUrl + 'api/test');
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(newEntity);
      req.flush(mockResponse);
    });
  });

  describe('update', () => {
    it('should update an existing entity', () => {
      const updatedEntity: Partial<TestEntity> = { name: 'Updated Test' };
      const mockResponse: TestEntity = { id: '1', name: 'Updated Test' };

      service.update('1', updatedEntity).subscribe(entity => {
        expect(entity).toEqual(mockResponse);
      });

      const req = httpMock.expectOne((service as any).baseUrl + 'api/test/1');
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(updatedEntity);
      req.flush(mockResponse);
    });
  });

  describe('delete', () => {
    it('should delete an entity', () => {
      service.delete('1').subscribe(() => {
        expect(true).toBe(true);
      });

      const req = httpMock.expectOne((service as any).baseUrl + 'api/test/1');
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });
  });
});
