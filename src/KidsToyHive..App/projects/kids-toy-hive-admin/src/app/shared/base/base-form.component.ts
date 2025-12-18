// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Directive, Input, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog.component';

/**
 * Abstract base class for entity detail forms
 * Provides common form functionality and validation
 */
@Directive()
export abstract class BaseFormComponent<T> implements OnInit, OnDestroy {
  @Input() item: T | null = null;
  @Input() mode: 'create' | 'edit' = 'create';
  @Input() loading = false;

  @Output() save = new EventEmitter<Partial<T>>();
  @Output() delete = new EventEmitter<T>();
  @Output() cancel = new EventEmitter<void>();

  protected readonly destroy$ = new Subject<void>();
  abstract form: FormGroup;

  constructor(protected readonly dialog: MatDialog) {}

  abstract ngOnInit(): void;

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Get form title based on mode
   */
  get title(): string {
    return this.mode === 'create' ? this.getCreateTitle() : this.getEditTitle();
  }

  protected abstract getCreateTitle(): string;
  protected abstract getEditTitle(): string;

  /**
   * Check if form is valid and dirty
   */
  get canSave(): boolean {
    return this.form.valid && this.form.dirty && !this.loading;
  }

  /**
   * Handle form submission
   */
  onSubmit(): void {
    if (this.form.valid) {
      this.save.emit(this.form.value);
    }
  }

  /**
   * Handle cancel action
   */
  onCancel(): void {
    if (this.form.dirty) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        data: {
          title: 'Discard Changes?',
          message: 'You have unsaved changes. Are you sure you want to discard them?',
          confirmText: 'Discard',
          cancelText: 'Keep Editing',
          confirmColor: 'warn'
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.form.reset();
          this.cancel.emit();
        }
      });
    } else {
      this.cancel.emit();
    }
  }

  /**
   * Handle delete action
   */
  onDelete(): void {
    if (!this.item) return;

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Delete Item?',
        message: this.getDeleteMessage(),
        confirmText: 'Delete',
        cancelText: 'Cancel',
        confirmColor: 'warn'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result && this.item) {
        this.delete.emit(this.item);
      }
    });
  }

  /**
   * Get custom delete confirmation message
   */
  protected getDeleteMessage(): string {
    return 'Are you sure you want to delete this item? This action cannot be undone.';
  }

  /**
   * Reset form to initial state
   */
  reset(): void {
    this.form.reset();
  }
}
