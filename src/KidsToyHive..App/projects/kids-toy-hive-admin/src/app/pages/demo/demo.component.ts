// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, inject, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseMasterDetailComponent } from '../../shared/base/base-master-detail.component';
import { MasterDetailComponent } from '../../shared/components/master-detail.component';
import { DemoFormComponent } from './demo-form.component';
import { DemoEntity } from './models/demo-entity';
import { DemoService } from './services/demo.service';

/**
 * Demo page demonstrating the master-detail pattern
 * This serves as an example for all entity management pages
 */
@Component({
  selector: 'app-demo',
  standalone: true,
  imports: [
    CommonModule,
    MasterDetailComponent,
    DemoFormComponent
  ],
  template: `
    <div class="demo-container">
      <h1>Demo: Master-Detail Pattern</h1>
      <p class="description">
        This page demonstrates the reusable master-detail CRUD pattern.
        Use this as a template for creating entity management pages.
      </p>

      <app-master-detail
        [items]="(filteredItems$ | async) || []"
        [selectedItem]="selectedItemSubject.value"
        [loading]="loadingSubject.value"
        [error]="errorSubject.value"
        [drawerOpened]="drawerOpened()"
        (itemSelect)="selectItem($event)"
        (itemCreate)="openCreateDialog()"
        (drawerClose)="deselectItem()"
        (searchChange)="onSearch($event)">
        
        <!-- Custom list item template -->
        <ng-template #itemTemplate let-item>
          <div class="custom-item">
            <div class="item-header">
              <strong>{{ item.name }}</strong>
              <span 
                class="status-badge" 
                [class.active]="item.isActive"
                [class.inactive]="!item.isActive">
                {{ item.isActive ? 'Active' : 'Inactive' }}
              </span>
            </div>
            <div class="item-description">{{ item.description }}</div>
            <div class="item-meta">
              <span class="category">{{ item.category }}</span>
              <span class="date">{{ item.createdDate | date:'shortDate' }}</span>
            </div>
          </div>
        </ng-template>

        <!-- Form section -->
        <div form>
          <app-demo-form
            [item]="selectedItemSubject.value"
            [mode]="formMode()"
            [loading]="loadingSubject.value"
            (save)="onSave($event)"
            (delete)="confirmDelete($event)"
            (cancel)="onCancel()">
          </app-demo-form>
        </div>
      </app-master-detail>
    </div>
  `,
  styles: [`
    .demo-container {
      height: 100vh;
      display: flex;
      flex-direction: column;
      padding: 0;
    }

    h1 {
      margin: 0;
      padding: 16px 24px 8px;
      font-size: 28px;
      font-weight: 400;
    }

    .description {
      margin: 0;
      padding: 0 24px 16px;
      color: rgba(0, 0, 0, 0.6);
      font-size: 14px;
    }

    app-master-detail {
      flex: 1;
      overflow: hidden;
    }

    .custom-item {
      display: flex;
      flex-direction: column;
      gap: 4px;
      padding: 8px 16px;
      width: 100%;
    }

    .item-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }

    .item-header strong {
      font-size: 16px;
    }

    .status-badge {
      padding: 2px 8px;
      border-radius: 12px;
      font-size: 11px;
      font-weight: 500;
      text-transform: uppercase;
    }

    .status-badge.active {
      background-color: #c8e6c9;
      color: #2e7d32;
    }

    .status-badge.inactive {
      background-color: #ffcdd2;
      color: #c62828;
    }

    .item-description {
      font-size: 14px;
      color: rgba(0, 0, 0, 0.6);
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }

    .item-meta {
      display: flex;
      justify-content: space-between;
      font-size: 12px;
      color: rgba(0, 0, 0, 0.4);
    }

    .category {
      font-weight: 500;
    }
  `]
})
export class DemoComponent extends BaseMasterDetailComponent<DemoEntity> {
  private readonly demoService = inject(DemoService);

  constructor() {
    super(inject(MatDialog), inject(MatSnackBar));
  }

  protected override getItems(): Observable<DemoEntity[]> {
    return this.demoService.getAll();
  }

  protected override createItem(item: Partial<DemoEntity>): Observable<DemoEntity> {
    return this.demoService.create(item);
  }

  protected override updateItem(id: string, item: Partial<DemoEntity>): Observable<DemoEntity> {
    return this.demoService.update(id, item);
  }

  protected override deleteItem(id: string): Observable<void> {
    return this.demoService.delete(id);
  }

  /**
   * Custom search implementation for demo entities
   */
  protected override matchesSearchTerm(item: DemoEntity, searchTerm: string): boolean {
    return (
      item.name.toLowerCase().includes(searchTerm) ||
      item.description.toLowerCase().includes(searchTerm) ||
      item.category.toLowerCase().includes(searchTerm)
    );
  }

  /**
   * Confirm delete with custom dialog
   */
  confirmDelete(item: DemoEntity): void {
    this.onDelete(item);
  }
}
