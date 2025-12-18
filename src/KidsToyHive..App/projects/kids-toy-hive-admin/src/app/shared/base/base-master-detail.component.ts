// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Directive, OnInit, OnDestroy, signal, computed } from '@angular/core';
import { BehaviorSubject, Subject, Observable, combineLatest } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged, map, catchError, tap, finalize } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

/**
 * Base state interface for master-detail components
 */
export interface BaseMasterDetailState<T> {
  items: T[];
  selectedItem: T | null;
  loading: boolean;
  error: string | null;
  searchTerm: string;
}

/**
 * Abstract base class for master-detail CRUD pattern
 * Provides common functionality for entity management pages
 * 
 * @template T - The entity type
 */
@Directive()
export abstract class BaseMasterDetailComponent<T extends { id: string }> implements OnInit, OnDestroy {
  protected readonly destroy$ = new Subject<void>();
  
  // State management
  protected readonly itemsSubject = new BehaviorSubject<T[]>([]);
  protected readonly selectedItemSubject = new BehaviorSubject<T | null>(null);
  protected readonly loadingSubject = new BehaviorSubject<boolean>(false);
  protected readonly errorSubject = new BehaviorSubject<string | null>(null);
  protected readonly searchTermSubject = new BehaviorSubject<string>('');

  // Public observables
  readonly items$ = this.itemsSubject.asObservable();
  readonly selectedItem$ = this.selectedItemSubject.asObservable();
  readonly loading$ = this.loadingSubject.asObservable();
  readonly error$ = this.errorSubject.asObservable();
  readonly searchTerm$ = this.searchTermSubject.asObservable();

  // Derived state - filtered items based on search
  readonly filteredItems$: Observable<T[]> = combineLatest([
    this.items$,
    this.searchTerm$.pipe(
      debounceTime(300),
      distinctUntilChanged()
    )
  ]).pipe(
    map(([items, searchTerm]) => this.filterItems(items, searchTerm))
  );

  // Signals for reactive template
  protected readonly drawerOpened = signal(false);
  protected readonly formMode = signal<'create' | 'edit'>('edit');

  // Computed values
  readonly isEmpty = computed(() => this.itemsSubject.value.length === 0);
  readonly hasSelection = computed(() => this.selectedItemSubject.value !== null);

  /**
   * Abstract methods to be implemented by derived classes
   */
  protected abstract getItems(): Observable<T[]>;
  protected abstract createItem(item: Partial<T>): Observable<T>;
  protected abstract updateItem(id: string, item: Partial<T>): Observable<T>;
  protected abstract deleteItem(id: string): Observable<void>;
  
  /**
   * Optional: Customize item filtering logic
   */
  protected filterItems(items: T[], searchTerm: string): T[] {
    if (!searchTerm || searchTerm.trim() === '') {
      return items;
    }
    const term = searchTerm.toLowerCase();
    return items.filter(item => this.matchesSearchTerm(item, term));
  }

  /**
   * Optional: Customize search matching logic
   * Default implementation searches in JSON representation
   */
  protected matchesSearchTerm(item: T, searchTerm: string): boolean {
    const itemStr = JSON.stringify(item).toLowerCase();
    return itemStr.includes(searchTerm);
  }

  constructor(
    protected readonly dialog: MatDialog,
    protected readonly snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadItems();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Load all items
   */
  loadItems(): void {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    this.getItems()
      .pipe(
        takeUntil(this.destroy$),
        tap(items => this.itemsSubject.next(items)),
        catchError(error => {
          this.errorSubject.next(error.message || 'Failed to load items');
          return [];
        }),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe();
  }

  /**
   * Select an item and open the detail drawer
   */
  selectItem(item: T): void {
    this.selectedItemSubject.next(item);
    this.formMode.set('edit');
    this.drawerOpened.set(true);
  }

  /**
   * Deselect item and close drawer
   */
  deselectItem(): void {
    this.selectedItemSubject.next(null);
    this.drawerOpened.set(false);
  }

  /**
   * Open create dialog
   */
  openCreateDialog(): void {
    this.selectedItemSubject.next(null);
    this.formMode.set('create');
    this.drawerOpened.set(true);
  }

  /**
   * Handle item save (create or update)
   */
  onSave(item: Partial<T>): void {
    const isUpdate = this.formMode() === 'edit' && this.selectedItemSubject.value;
    
    this.loadingSubject.next(true);
    
    const operation$ = isUpdate 
      ? this.updateItem(this.selectedItemSubject.value!.id, item)
      : this.createItem(item);

    operation$
      .pipe(
        takeUntil(this.destroy$),
        tap(savedItem => {
          // Optimistic update
          const currentItems = this.itemsSubject.value;
          if (isUpdate) {
            const index = currentItems.findIndex(i => i.id === savedItem.id);
            if (index !== -1) {
              currentItems[index] = savedItem;
              this.itemsSubject.next([...currentItems]);
            }
          } else {
            this.itemsSubject.next([savedItem, ...currentItems]);
          }
          
          this.snackBar.open(
            `Item ${isUpdate ? 'updated' : 'created'} successfully`,
            'Close',
            { duration: 3000 }
          );
          
          this.deselectItem();
        }),
        catchError(error => {
          this.snackBar.open(
            error.message || `Failed to ${isUpdate ? 'update' : 'create'} item`,
            'Close',
            { duration: 5000 }
          );
          throw error;
        }),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe();
  }

  /**
   * Handle item deletion
   */
  onDelete(item: T): void {
    this.loadingSubject.next(true);

    this.deleteItem(item.id)
      .pipe(
        takeUntil(this.destroy$),
        tap(() => {
          // Optimistic update - remove from list
          const currentItems = this.itemsSubject.value;
          const updatedItems = currentItems.filter(i => i.id !== item.id);
          this.itemsSubject.next(updatedItems);
          
          this.snackBar.open('Item deleted successfully', 'Close', { duration: 3000 });
          this.deselectItem();
        }),
        catchError(error => {
          this.snackBar.open(
            error.message || 'Failed to delete item',
            'Close',
            { duration: 5000 }
          );
          throw error;
        }),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe();
  }

  /**
   * Handle search term update
   */
  onSearch(searchTerm: string): void {
    this.searchTermSubject.next(searchTerm);
  }

  /**
   * Handle cancel action
   */
  onCancel(): void {
    this.deselectItem();
  }
}
