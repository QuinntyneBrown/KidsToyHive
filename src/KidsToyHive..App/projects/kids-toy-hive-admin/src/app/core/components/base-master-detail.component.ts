// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnInit, OnDestroy, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDrawer } from '@angular/material/sidenav';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Subject, Observable } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { BaseEntity } from '../models/base-entity.model';
import { BaseHttpService } from '../services/base-http.service';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';

export interface ListItemConfig {
  primaryText: (item: any) => string;
  secondaryText?: (item: any) => string;
  tertiaryText?: (item: any) => string;
}

/**
 * Abstract base component for master-detail CRUD operations
 * @template T The entity type this component manages
 */
@Component({
  template: '',
  standalone: true
})
export abstract class BaseMasterDetailComponent<T extends BaseEntity> implements OnInit, OnDestroy {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly snackBar = inject(MatSnackBar);
  protected readonly dialog = inject(MatDialog);
  
  @ViewChild('drawer') drawer!: MatDrawer;
  
  // Observable streams
  protected readonly destroy$ = new Subject<void>();
  protected readonly itemsSubject = new BehaviorSubject<T[]>([]);
  protected readonly filteredItemsSubject = new BehaviorSubject<T[]>([]);
  protected readonly loadingSubject = new BehaviorSubject<boolean>(false);
  protected readonly errorSubject = new BehaviorSubject<string | null>(null);
  
  public readonly items$ = this.itemsSubject.asObservable();
  public readonly filteredItems$ = this.filteredItemsSubject.asObservable();
  public readonly loading$ = this.loadingSubject.asObservable();
  public readonly error$ = this.errorSubject.asObservable();
  
  // State
  public selectedItem: T | null = null;
  public searchControl = this.formBuilder.control('');
  public itemForm: FormGroup = this.formBuilder.group({});
  public isCreating = false;
  public isSaving = false;
  
  /**
   * The service to use for HTTP operations
   */
  protected abstract readonly service: BaseHttpService<T>;
  
  /**
   * The name of the entity (e.g., 'Product', 'Brand')
   */
  protected abstract readonly entityName: string;
  
  /**
   * Configuration for how to display list items
   */
  protected abstract readonly listItemConfig: ListItemConfig;
  
  /**
   * Create the form group for the entity
   */
  protected abstract createForm(item?: T): FormGroup;
  
  /**
   * Build entity from form values
   */
  protected abstract buildEntity(formValue: any): Partial<T>;
  
  ngOnInit(): void {
    this.setupSearchFilter();
    this.loadItems();
  }
  
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  
  /**
   * Setup search filter with debouncing
   */
  private setupSearchFilter(): void {
    this.searchControl.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        takeUntil(this.destroy$)
      )
      .subscribe(searchTerm => {
        this.filterItems(searchTerm || '');
      });
  }
  
  /**
   * Filter items based on search term
   */
  private filterItems(searchTerm: string): void {
    const items = this.itemsSubject.value;
    if (!searchTerm) {
      this.filteredItemsSubject.next(items);
      return;
    }
    
    const filtered = items.filter(item => 
      this.matchesSearchTerm(item, searchTerm.toLowerCase())
    );
    this.filteredItemsSubject.next(filtered);
  }
  
  /**
   * Override this method to customize search matching logic
   */
  protected matchesSearchTerm(item: T, searchTerm: string): boolean {
    const primaryText = this.listItemConfig.primaryText(item);
    const secondaryText = this.listItemConfig.secondaryText?.(item) || '';
    const tertiaryText = this.listItemConfig.tertiaryText?.(item) || '';
    
    return primaryText.toLowerCase().includes(searchTerm) ||
           secondaryText.toLowerCase().includes(searchTerm) ||
           tertiaryText.toLowerCase().includes(searchTerm);
  }
  
  /**
   * Load all items from the service
   */
  loadItems(): void {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);
    
    this.service.getAll()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (items) => {
          this.itemsSubject.next(items);
          this.filteredItemsSubject.next(items);
          this.loadingSubject.next(false);
        },
        error: (error) => {
          this.errorSubject.next(error.message || 'Failed to load items');
          this.loadingSubject.next(false);
        }
      });
  }
  
  /**
   * Select an item and open the form
   */
  selectItem(item: T): void {
    this.selectedItem = item;
    this.isCreating = false;
    this.itemForm = this.createForm(item);
    this.drawer?.open();
  }
  
  /**
   * Open the create dialog/form
   */
  openCreateDialog(): void {
    this.selectedItem = null;
    this.isCreating = true;
    this.itemForm = this.createForm();
    this.drawer?.open();
  }
  
  /**
   * Save the current item (create or update)
   */
  save(): void {
    if (this.itemForm.invalid || this.isSaving) {
      return;
    }
    
    this.isSaving = true;
    const entity = this.buildEntity(this.itemForm.value);
    
    const operation: Observable<T> = this.isCreating
      ? this.service.create(entity)
      : this.service.update(this.selectedItem?.id ?? '', entity);
    
    operation
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (savedItem) => {
          const action = this.isCreating ? 'created' : 'updated';
          this.snackBar.open(`${this.entityName} ${action} successfully`, 'Close', {
            duration: 3000
          });
          
          this.loadItems();
          this.closeForm();
          this.isSaving = false;
        },
        error: (error) => {
          this.snackBar.open(error.message || `Failed to save ${this.entityName}`, 'Close', {
            duration: 5000
          });
          this.isSaving = false;
        }
      });
  }
  
  /**
   * Delete the selected item
   */
  delete(): void {
    if (!this.selectedItem?.id) {
      return;
    }
    
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: `Delete ${this.entityName}`,
        message: `Are you sure you want to delete this ${this.entityName.toLowerCase()}? This action cannot be undone.`,
        confirmText: 'Delete',
        cancelText: 'Cancel',
        confirmColor: 'warn'
      }
    });
    
    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed && this.selectedItem?.id) {
        this.performDelete(this.selectedItem.id);
      }
    });
  }
  
  /**
   * Perform the delete operation
   */
  private performDelete(id: string): void {
    this.service.delete(id)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          this.snackBar.open(`${this.entityName} deleted successfully`, 'Close', {
            duration: 3000
          });
          this.loadItems();
          this.closeForm();
        },
        error: (error) => {
          this.snackBar.open(error.message || `Failed to delete ${this.entityName}`, 'Close', {
            duration: 5000
          });
        }
      });
  }
  
  /**
   * Close the form drawer
   */
  closeForm(): void {
    if (this.itemForm.dirty) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '400px',
        data: {
          title: 'Unsaved Changes',
          message: 'You have unsaved changes. Are you sure you want to close?',
          confirmText: 'Close',
          cancelText: 'Stay',
          confirmColor: 'warn'
        }
      });
      
      dialogRef.afterClosed().subscribe(confirmed => {
        if (confirmed) {
          this.doCloseForm();
        }
      });
    } else {
      this.doCloseForm();
    }
  }
  
  /**
   * Actually close the form
   */
  private doCloseForm(): void {
    this.drawer?.close();
    this.selectedItem = null;
    this.isCreating = false;
    this.itemForm.reset();
  }
  
  /**
   * Cancel form editing
   */
  cancel(): void {
    this.closeForm();
  }
  
  /**
   * Check if form is valid and changed
   */
  canSave(): boolean {
    return this.itemForm.valid && this.itemForm.dirty && !this.isSaving;
  }
  
  /**
   * Get primary display text for an item
   */
  getPrimaryText(item: T): string {
    return this.listItemConfig.primaryText(item);
  }
  
  /**
   * Get secondary display text for an item
   */
  getSecondaryText(item: T): string {
    return this.listItemConfig.secondaryText?.(item) || '';
  }
  
  /**
   * Get tertiary display text for an item
   */
  getTertiaryText(item: T): string {
    return this.listItemConfig.tertiaryText?.(item) || '';
  }
  
  /**
   * Check if item is selected
   */
  isSelected(item: T): boolean {
    return this.selectedItem?.id === item.id;
  }
  
  /**
   * Get count message
   */
  getCountMessage(): string {
    const filtered = this.filteredItemsSubject.value.length;
    const total = this.itemsSubject.value.length;
    
    if (filtered === total) {
      return `Showing ${total} ${total === 1 ? 'item' : 'items'}`;
    }
    return `Showing ${filtered} of ${total} items`;
  }
  
  /**
   * Check if there are no items
   */
  isEmpty(): boolean {
    return this.itemsSubject.value.length === 0;
  }
}
