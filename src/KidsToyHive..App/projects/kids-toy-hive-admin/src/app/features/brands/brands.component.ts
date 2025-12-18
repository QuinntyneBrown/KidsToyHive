// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BaseMasterDetailComponent, ListItemConfig } from '../../core/components';
import { Brand } from './models/brand.model';
import { BrandService } from './services/brand.service';

@Component({
  selector: 'app-brands',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatTooltipModule
  ],
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.scss']
})
export class BrandsComponent extends BaseMasterDetailComponent<Brand> {
  protected readonly service = inject(BrandService);
  protected readonly entityName = 'Brand';
  
  protected readonly listItemConfig: ListItemConfig = {
    primaryText: (brand: Brand) => brand.name,
    secondaryText: (brand: Brand) => brand.description || '',
    tertiaryText: (brand: Brand) => brand.websiteUrl || ''
  };
  
  protected createForm(brand?: Brand) {
    return this.formBuilder.group({
      name: [brand?.name || '', [Validators.required, Validators.maxLength(100)]],
      description: [brand?.description || '', Validators.maxLength(500)],
      logoUrl: [brand?.logoUrl || '', Validators.maxLength(500)],
      websiteUrl: [brand?.websiteUrl || '', Validators.maxLength(500)],
      isActive: [brand?.isActive ?? true]
    });
  }
  
  protected buildEntity(formValue: any): Partial<Brand> {
    return {
      name: formValue.name,
      description: formValue.description,
      logoUrl: formValue.logoUrl,
      websiteUrl: formValue.websiteUrl,
      isActive: formValue.isActive
    };
  }
}

