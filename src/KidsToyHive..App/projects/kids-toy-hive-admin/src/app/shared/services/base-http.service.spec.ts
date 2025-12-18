// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { BaseHttpService } from './base-http.service';
import { HttpClient } from '@angular/common/http';

interface TestEntity {
  id: string;
  name: string;
}

class TestHttpService extends BaseHttpService<TestEntity> {
  protected get endpoint(): string {
    return 'api/test-entities';
  }

  constructor(http: HttpClient, baseUrl: string) {
    super(http, baseUrl);
  }
}

describe('BaseHttpService', () => {
  let service: TestHttpService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://localhost:5000/';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    const http = TestBed.inject(HttpClient);
    service = new TestHttpService(http, baseUrl);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getAll', () => {
    it('should fetch all entities', (done) => {
      const mockEntities: TestEntity[] = [
        { id: '1', name: 'Test 1' },
        { id: '2', name: 'Test 2' }
      ];

      service.getAll().subscribe({
        next: (entities) => {
          expect(entities).toEqual(mockEntities);
          expect(entities.length).toBe(2);
          done();
        }
      });

      const req = httpMock.expectOne(`${baseUrl}api/test-entities`);
      expect(req.request.method).toBe('GET');
      req.flush(mockEntities);
    });

    it('should handle error', (done) => {
      service.getAll().subscribe({
        error: (error) => {
          expect(error).toBeTruthy();
          expect(error.message).toContain('Error');
          done();
        }
      });

      const req = httpMock.expectOne(`${baseUrl}api/test-entities`);
      req.flush({ message: 'Server error' }, { status: 500, statusText: 'Server Error' });
    });
  });

  describe('getById', () => {
    it('should fetch entity by id', (done) => {
      const mockEntity: TestEntity = { id: '1', name: 'Test 1' };

      service.getById('1').subscribe({
        next: (entity) => {
          expect(entity).toEqual(mockEntity);
          done();
        }
      });

      const req = httpMock.expectOne(`${baseUrl}api/test-entities/1`);
      expect(req.request.method).toBe('GET');
      req.flush(mockEntity);
    });
  });

  describe('create', () => {
    it('should create new entity', (done) => {
      const newEntity: Partial<TestEntity> = { name: 'New Test' };
      const createdEntity: TestEntity = { id: '3', name: 'New Test' };

      service.create(newEntity).subscribe({
        next: (entity) => {
          expect(entity).toEqual(createdEntity);
          done();
        }
      });

      const req = httpMock.expectOne(`${baseUrl}api/test-entities`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(newEntity);
      req.flush(createdEntity);
    });
  });

  describe('update', () => {
    it('should update existing entity', (done) => {
      const updatedEntity: Partial<TestEntity> = { name: 'Updated Test' };
      const resultEntity: TestEntity = { id: '1', name: 'Updated Test' };

      service.update('1', updatedEntity).subscribe({
        next: (entity) => {
          expect(entity).toEqual(resultEntity);
          done();
        }
      });

      const req = httpMock.expectOne(`${baseUrl}api/test-entities/1`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(updatedEntity);
      req.flush(resultEntity);
    });
  });

  describe('delete', () => {
    it('should delete entity', (done) => {
      service.delete('1').subscribe({
        next: () => {
          expect(true).toBe(true);
          done();
        }
      });

      const req = httpMock.expectOne(`${baseUrl}api/test-entities/1`);
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });
  });
});
