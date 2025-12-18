// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, Input, Output, EventEmitter, ContentChild, TemplateRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormsModule } from '@angular/forms';
import { ScrollingModule } from '@angular/cdk/scrolling';

/**
 * Generic master-detail component for displaying and managing entities
 * Provides split layout with list on left and detail form on right
 */
@Component({
  selector: 'app-master-detail',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    ScrollingModule
  ],
  template: `
    <mat-drawer-container class="master-detail-container" [hasBackdrop]="false">
      <!-- List Section -->
      <mat-drawer-content class="list-section">
        <!-- Search Bar -->
        <div class="search-container">
          <mat-form-field class="search-field" appearance="outline">
            <mat-label>Search</mat-label>
            <input 
              matInput 
              [(ngModel)]="searchTerm"
              (ngModelChange)="onSearchChange($event)"
              placeholder="Search items...">
            <mat-icon matSuffix>search</mat-icon>
          </mat-form-field>
        </div>

        <!-- Loading State -->
        <div *ngIf="loading" class="loading-container">
          <mat-spinner diameter="50"></mat-spinner>
        </div>

        <!-- Error State -->
        <div *ngIf="error && !loading" class="error-container">
          <mat-icon color="warn">error</mat-icon>
          <p>{{ error }}</p>
        </div>

        <!-- Empty State -->
        <div *ngIf="!loading && !error && items.length === 0" class="empty-container">
          <mat-icon>inbox</mat-icon>
          <p>No items found</p>
        </div>

        <!-- Item Count -->
        <div *ngIf="!loading && items.length > 0" class="item-count">
          Showing {{ items.length }} items
        </div>

        <!-- Items List -->
        <cdk-virtual-scroll-viewport 
          *ngIf="!loading && !error && items.length > 0"
          itemSize="72" 
          class="items-list">
          <mat-list>
            <mat-list-item 
              *cdkVirtualFor="let item of items"
              [class.selected]="selectedItem?.id === item.id"
              (click)="onItemSelect(item)"
              (dblclick)="onItemSelect(item)">
              <ng-container *ngIf="itemTemplate; else defaultItem">
                <ng-container *ngTemplateOutlet="itemTemplate; context: { $implicit: item }"></ng-container>
              </ng-container>
              <ng-template #defaultItem>
                <div class="list-item-content">
                  <span class="item-primary">{{ getItemDisplay(item) }}</span>
                </div>
              </ng-template>
            </mat-list-item>
          </mat-list>
        </cdk-virtual-scroll-viewport>

        <!-- Create Button (FAB) -->
        <button 
          mat-fab 
          color="primary" 
          class="create-fab"
          (click)="onCreate()"
          aria-label="Create new item">
          <mat-icon>add</mat-icon>
        </button>
      </mat-drawer-content>

      <!-- Detail Form Section -->
      <mat-drawer 
        #drawer
        mode="side" 
        position="end"
        [opened]="drawerOpened"
        (closed)="onDrawerClose()">
        <div class="form-container">
          <ng-content select="[form]"></ng-content>
        </div>
      </mat-drawer>
    </mat-drawer-container>
  `,
  styles: [`
    .master-detail-container {
      height: 100%;
      width: 100%;
    }

    .list-section {
      display: flex;
      flex-direction: column;
      height: 100%;
      position: relative;
      background-color: #fafafa;
    }

    .search-container {
      padding: 16px;
      background-color: white;
      border-bottom: 1px solid #e0e0e0;
    }

    .search-field {
      width: 100%;
    }

    .item-count {
      padding: 8px 16px;
      font-size: 12px;
      color: rgba(0, 0, 0, 0.6);
      background-color: white;
      border-bottom: 1px solid #e0e0e0;
    }

    .items-list {
      flex: 1;
      overflow-y: auto;
    }

    mat-list-item {
      cursor: pointer;
      transition: background-color 0.2s;
      border-bottom: 1px solid #e0e0e0;
    }

    mat-list-item:hover {
      background-color: rgba(0, 0, 0, 0.04);
    }

    mat-list-item.selected {
      background-color: rgba(63, 81, 181, 0.12);
    }

    .list-item-content {
      display: flex;
      flex-direction: column;
      width: 100%;
      padding: 8px 0;
    }

    .item-primary {
      font-weight: 500;
    }

    .create-fab {
      position: fixed;
      bottom: 24px;
      right: 24px;
      z-index: 10;
    }

    mat-drawer {
      width: 600px;
      max-width: 90vw;
    }

    .form-container {
      height: 100%;
      overflow-y: auto;
    }

    .loading-container,
    .error-container,
    .empty-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 48px 16px;
      text-align: center;
    }

    .loading-container mat-spinner {
      margin-bottom: 16px;
    }

    .error-container mat-icon,
    .empty-container mat-icon {
      font-size: 64px;
      width: 64px;
      height: 64px;
      margin-bottom: 16px;
      opacity: 0.5;
    }

    .error-container p,
    .empty-container p {
      color: rgba(0, 0, 0, 0.6);
      margin: 0;
    }

    /* Responsive Design */
    @media (max-width: 960px) {
      mat-drawer {
        width: 100vw;
        max-width: 100vw;
      }

      .create-fab {
        bottom: 16px;
        right: 16px;
      }
    }

    @media (min-width: 961px) {
      .list-section {
        width: 400px;
        min-width: 400px;
      }
    }
  `]
})
export class MasterDetailComponent<T extends { id: string }> {
  @Input() items: T[] = [];
  @Input() selectedItem: T | null = null;
  @Input() loading = false;
  @Input() error: string | null = null;
  @Input() drawerOpened = false;
  
  @Output() itemSelect = new EventEmitter<T>();
  @Output() itemCreate = new EventEmitter<void>();
  @Output() drawerClose = new EventEmitter<void>();
  @Output() searchChange = new EventEmitter<string>();

  @ContentChild('itemTemplate') itemTemplate?: TemplateRef<any>;

  searchTerm = '';

  onItemSelect(item: T): void {
    this.itemSelect.emit(item);
  }

  onCreate(): void {
    this.itemCreate.emit();
  }

  onDrawerClose(): void {
    this.drawerClose.emit();
  }

  onSearchChange(term: string): void {
    this.searchChange.emit(term);
  }

  /**
   * Default display for items
   * Override by providing itemTemplate
   */
  getItemDisplay(item: T): string {
    // Try common property names
    const displayProps = ['name', 'title', 'displayName'];
    for (const prop of displayProps) {
      if (prop in item && typeof (item as any)[prop] === 'string') {
        return (item as any)[prop];
      }
    }
    return item.id;
  }
}
