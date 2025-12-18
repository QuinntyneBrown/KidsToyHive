// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { BaseFormComponent } from '../../shared/base/base-form.component';
import { DemoEntity } from './models/demo-entity';

/**
 * Form component for DemoEntity
 * Demonstrates usage of BaseFormComponent
 */
@Component({
  selector: 'app-demo-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-toolbar color="primary">
        <span>{{ title }}</span>
        <span class="spacer"></span>
        <button 
          *ngIf="mode === 'edit'"
          mat-icon-button 
          type="button"
          (click)="onDelete()"
          [disabled]="loading">
          <mat-icon>delete</mat-icon>
        </button>
      </mat-toolbar>

      <div class="form-content">
        <mat-form-field appearance="outline">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
          <mat-error *ngIf="form.get('name')?.hasError('required')">
            Name is required
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Description</mat-label>
          <textarea 
            matInput 
            formControlName="description" 
            rows="4"
            required></textarea>
          <mat-error *ngIf="form.get('description')?.hasError('required')">
            Description is required
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Category</mat-label>
          <mat-select formControlName="category" required>
            <mat-option value="Category A">Category A</mat-option>
            <mat-option value="Category B">Category B</mat-option>
            <mat-option value="Category C">Category C</mat-option>
          </mat-select>
          <mat-error *ngIf="form.get('category')?.hasError('required')">
            Category is required
          </mat-error>
        </mat-form-field>

        <div class="toggle-field">
          <mat-slide-toggle formControlName="isActive">
            Active
          </mat-slide-toggle>
        </div>

        <mat-form-field appearance="outline">
          <mat-label>Created Date</mat-label>
          <input 
            matInput 
            [matDatepicker]="picker"
            formControlName="createdDate"
            required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
          <mat-error *ngIf="form.get('createdDate')?.hasError('required')">
            Created date is required
          </mat-error>
        </mat-form-field>
      </div>

      <div class="form-actions">
        <button 
          mat-button 
          type="button" 
          (click)="onCancel()"
          [disabled]="loading">
          Cancel
        </button>
        <button 
          mat-raised-button 
          color="primary" 
          type="submit"
          [disabled]="!canSave">
          Save
        </button>
      </div>
    </form>
  `,
  styles: [`
    :host {
      display: block;
      height: 100%;
    }

    form {
      display: flex;
      flex-direction: column;
      height: 100%;
    }

    .spacer {
      flex: 1 1 auto;
    }

    .form-content {
      flex: 1;
      padding: 24px;
      overflow-y: auto;
      display: flex;
      flex-direction: column;
      gap: 16px;
    }

    mat-form-field {
      width: 100%;
    }

    .toggle-field {
      padding: 8px 0;
    }

    .form-actions {
      display: flex;
      justify-content: flex-end;
      gap: 8px;
      padding: 16px 24px;
      border-top: 1px solid #e0e0e0;
      background-color: #fafafa;
    }
  `]
})
export class DemoFormComponent extends BaseFormComponent<DemoEntity> implements OnInit {
  private readonly fb = inject(FormBuilder);

  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    category: ['', Validators.required],
    isActive: [true],
    createdDate: [new Date(), Validators.required]
  });

  constructor() {
    super(inject(MatDialog));
  }

  override ngOnInit(): void {
    if (this.item) {
      this.form.patchValue({
        name: this.item.name,
        description: this.item.description,
        category: this.item.category,
        isActive: this.item.isActive,
        createdDate: this.item.createdDate
      });
    }
  }

  protected override getCreateTitle(): string {
    return 'Create Demo Entity';
  }

  protected override getEditTitle(): string {
    return `Edit: ${this.item?.name || 'Demo Entity'}`;
  }

  protected override getDeleteMessage(): string {
    return `Are you sure you want to delete "${this.item?.name}"? This action cannot be undone.`;
  }
}
