// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, Input, Output, EventEmitter, TemplateRef, ContentChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-master-detail-template',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatToolbarModule,
    MatCardModule
  ],
  templateUrl: './master-detail-template.component.html',
  styleUrls: ['./master-detail-template.component.scss']
})
export class MasterDetailTemplateComponent<T> {
  @Input() items: T[] = [];
  @Input() selectedItem: T | null = null;
  @Input() loading = false;
  @Input() searchControl!: FormControl;
  @Input() entityName = 'Item';
  @Input() countMessage = '';
  @Input() isEmpty = false;
  
  @Output() itemSelected = new EventEmitter<T>();
  @Output() createClicked = new EventEmitter<void>();
  
  @ContentChild('listItem') listItemTemplate!: TemplateRef<any>;
  @ContentChild('detailForm') detailFormTemplate!: TemplateRef<any>;
  @ContentChild('emptyState') emptyStateTemplate!: TemplateRef<any>;
  
  onItemClick(item: T): void {
    this.itemSelected.emit(item);
  }
  
  onCreateClick(): void {
    this.createClicked.emit();
  }
  
  isSelected(item: any): boolean {
    return this.selectedItem === item;
  }
}
