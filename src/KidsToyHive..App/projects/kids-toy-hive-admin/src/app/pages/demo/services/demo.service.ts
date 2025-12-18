// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, delay } from 'rxjs';
import { BaseHttpService } from '../../../shared/services/base-http.service';
import { DemoEntity } from '../models/demo-entity';
import { environment } from '../../../../environments/environment';

/**
 * Demo service using BaseHttpService
 * In production, this would connect to real API endpoints
 */
@Injectable({
  providedIn: 'root'
})
export class DemoService extends BaseHttpService<DemoEntity> {
  protected override get endpoint(): string {
    return 'api/demo-entities';
  }

  constructor() {
    super(inject(HttpClient), environment.baseUrl);
  }

  /**
   * Mock implementation for demonstration
   * In real application, remove this and use inherited methods
   */
  override getAll(): Observable<DemoEntity[]> {
    // Mock data for demonstration
    const mockData: DemoEntity[] = [
      {
        id: '1',
        name: 'Demo Item 1',
        description: 'This is a demo item for testing',
        category: 'Category A',
        isActive: true,
        createdDate: new Date('2024-01-01')
      },
      {
        id: '2',
        name: 'Demo Item 2',
        description: 'Another demo item',
        category: 'Category B',
        isActive: false,
        createdDate: new Date('2024-01-15')
      },
      {
        id: '3',
        name: 'Demo Item 3',
        description: 'Third demo item for variety',
        category: 'Category A',
        isActive: true,
        createdDate: new Date('2024-02-01')
      }
    ];

    return of(mockData).pipe(delay(500)); // Simulate network delay
  }

  override create(entity: Partial<DemoEntity>): Observable<DemoEntity> {
    const newEntity: DemoEntity = {
      id: Math.random().toString(36).substr(2, 9),
      name: entity.name || '',
      description: entity.description || '',
      category: entity.category || '',
      isActive: entity.isActive ?? true,
      createdDate: new Date()
    };
    return of(newEntity).pipe(delay(300));
  }

  override update(id: string, entity: Partial<DemoEntity>): Observable<DemoEntity> {
    const updated: DemoEntity = {
      id,
      name: entity.name || '',
      description: entity.description || '',
      category: entity.category || '',
      isActive: entity.isActive ?? true,
      createdDate: entity.createdDate || new Date()
    };
    return of(updated).pipe(delay(300));
  }

  override delete(id: string): Observable<void> {
    return of(void 0).pipe(delay(300));
  }
}
