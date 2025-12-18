# Master-Detail CRUD Pattern

This document describes the reusable master-detail CRUD pattern implemented in the kids-toy-hive-admin application.

## Overview

The master-detail pattern provides a consistent, rich user experience for managing entities with CRUD operations. It features:

- **Split Layout**: List of items on the left (400px on desktop, full width on mobile), detail form on the right
- **Responsive Design**: Automatically adapts to different screen sizes
- **Rich Interactions**: Search, filter, keyboard navigation, and smooth animations
- **Form Validation**: Comprehensive validation with error messages
- **State Management**: Loading, error, and empty states
- **Confirmation Dialogs**: For unsaved changes and delete operations

## Architecture

### Base Components

1. **BaseMasterDetailComponent<T>** - Abstract base class for master-detail views
2. **BaseHttpService<T>** - Abstract base class for HTTP CRUD operations
3. **ConfirmDialogComponent** - Reusable confirmation dialog
4. **MasterDetailTemplateComponent<T>** - Optional template component for common layouts

### Models

- **BaseEntity** - Base interface for all entities (id, createdAt, updatedAt)

## Quick Start

### 1. Create Your Entity Model

```typescript
// models/product.model.ts
import { BaseEntity } from '../../../core/models';

export interface Product extends BaseEntity {
  name: string;
  description?: string;
  price: number;
  category: string;
  isActive: boolean;
}
```

### 2. Create Your Service

```typescript
// services/product.service.ts
import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../core/services';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseHttpService<Product> {
  protected readonly endpoint = 'api/products';
}
```

### 3. Create Your Component

```typescript
// products.component.ts
import { Component, inject } from '@angular/core';
import { Validators } from '@angular/forms';
import { BaseMasterDetailComponent, ListItemConfig } from '../../core/components';
import { Product } from './models/product.model';
import { ProductService } from './services/product.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [/* Material modules */],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseMasterDetailComponent<Product> {
  protected readonly service = inject(ProductService);
  protected readonly entityName = 'Product';
  
  protected readonly listItemConfig: ListItemConfig = {
    primaryText: (product: Product) => product.name,
    secondaryText: (product: Product) => product.description || '',
    tertiaryText: (product: Product) => `$${product.price}`
  };
  
  protected createForm(product?: Product) {
    return this.formBuilder.group({
      name: [product?.name || '', [Validators.required, Validators.maxLength(100)]],
      description: [product?.description || '', Validators.maxLength(500)],
      price: [product?.price || 0, [Validators.required, Validators.min(0)]],
      category: [product?.category || '', Validators.required],
      isActive: [product?.isActive ?? true]
    });
  }
  
  protected buildEntity(formValue: any): Partial<Product> {
    return {
      name: formValue.name,
      description: formValue.description,
      price: formValue.price,
      category: formValue.category,
      isActive: formValue.isActive
    };
  }
}
```

### 4. Create Your Template

See `brands.component.html` for a complete example template. Key sections include:

- **Search Bar**: With debounced filtering
- **Item List**: With selection, hover effects, and keyboard navigation
- **Loading/Empty States**: User-friendly feedback
- **Detail Form**: With validation and error messages
- **Form Actions**: Save, Cancel, and Delete buttons
- **Create FAB**: Floating action button

### 5. Create Your Styles

See `brands.component.scss` for a complete example stylesheet. Key features:

- Responsive layout (desktop/mobile)
- List item styling with hover and selection states
- Form layout with proper spacing
- Animations for drawer open/close
- Loading and empty state styling

## Features

### List View Features

- ✅ Vertical scrollable container
- ✅ Material list items with elevation on hover
- ✅ Selected item highlighting (primary color background)
- ✅ Configurable display properties (primary, secondary, tertiary text)
- ✅ Search filter at top with 300ms debouncing
- ✅ Real-time filtering
- ✅ Item count display ("Showing X of Y items")
- ✅ Empty state ("No items found")
- ✅ Loading state with spinner
- ✅ Create button (FAB) at bottom-right
- ✅ Keyboard navigation support
- ✅ Double-click support

### Detail Form Features

- ✅ Right panel display
- ✅ Reactive forms with FormBuilder
- ✅ Angular Material form controls (mat-form-field, mat-select, mat-checkbox, etc.)
- ✅ Validation with mat-error messages
- ✅ Save button (disabled when invalid/unchanged)
- ✅ Cancel button
- ✅ Delete button in form header
- ✅ Dynamic header (entity name or "New [Entity]")
- ✅ Unsaved changes warning
- ✅ Disabled fields during save
- ✅ Success messages (mat-snackbar)
- ✅ Form reset after save

### Dialog Features

- ✅ Delete confirmation dialog
- ✅ Unsaved changes confirmation
- ✅ Warning messages
- ✅ Styled action buttons
- ✅ Cancel and confirm actions

## Customization

### Custom Search Logic

Override the `matchesSearchTerm` method:

```typescript
protected matchesSearchTerm(item: Product, searchTerm: string): boolean {
  return item.name.toLowerCase().includes(searchTerm) ||
         item.category.toLowerCase().includes(searchTerm) ||
         item.sku?.toLowerCase().includes(searchTerm);
}
```

### Additional Form Controls

Add custom form controls in your template:

```html
<mat-form-field appearance="outline" class="full-width">
  <mat-label>Category</mat-label>
  <mat-select formControlName="category">
    <mat-option value="toys">Toys</mat-option>
    <mat-option value="games">Games</mat-option>
  </mat-select>
</mat-form-field>

<mat-form-field appearance="outline" class="full-width">
  <mat-label>Release Date</mat-label>
  <input matInput [matDatepicker]="picker" formControlName="releaseDate">
  <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
  <mat-datepicker #picker></mat-datepicker>
</mat-form-field>
```

### Custom Empty State

Provide a custom empty state template:

```html
<div class="empty-state">
  <img src="assets/empty-products.svg" alt="No products">
  <h3>No Products Yet</h3>
  <p>Start building your catalog by adding products</p>
  <button mat-raised-button color="primary" (click)="openCreateDialog()">
    Add Your First Product
  </button>
</div>
```

## Best Practices

1. **Always extend BaseMasterDetailComponent** - Don't copy-paste the logic
2. **Use the BaseHttpService** - Ensures consistent API communication
3. **Follow the template structure** - Maintain UI consistency across features
4. **Implement proper validation** - Use Angular validators in createForm()
5. **Provide meaningful error messages** - Help users fix validation errors
6. **Test your components** - Write unit tests for services and components
7. **Keep entity-specific logic separate** - Don't modify base classes

## Example: Brands Feature

The Brands feature (`features/brands/`) serves as a reference implementation demonstrating all pattern features:

- Full CRUD operations
- Search and filtering
- Validation
- Responsive design
- Confirmation dialogs
- State management

Study this implementation when creating new features.

## Testing

### Service Testing

```typescript
import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ProductService } from './product.service';

describe('ProductService', () => {
  let service: ProductService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService]
    });
    service = TestBed.inject(ProductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
```

### Component Testing

See `confirm-dialog.component.spec.ts` for an example of component testing.

## Troubleshooting

### Build Errors

- Ensure all Material modules are imported
- Check that your entity extends BaseEntity
- Verify service extends BaseHttpService<T>
- Confirm component extends BaseMasterDetailComponent<T>

### Runtime Errors

- Verify API endpoint is correct in service
- Check environment.baseUrl configuration
- Ensure HTTP interceptors are configured
- Verify form field names match entity properties

## Support

For questions or issues with the master-detail pattern:

1. Review the Brands example implementation
2. Check this documentation
3. Review base component source code
4. Contact the development team

## Future Enhancements

Potential improvements to the pattern:

- [ ] Pagination support for large datasets
- [ ] Bulk operations (select multiple, delete multiple)
- [ ] Export functionality (CSV, Excel)
- [ ] Advanced filtering (date ranges, multi-select)
- [ ] Sorting options (by column)
- [ ] Inline editing in list view
- [ ] Drag-and-drop reordering
- [ ] Print preview

---

**Version**: 1.0.0  
**Last Updated**: 2025-12-18  
**Maintainer**: KidsToyHive Development Team
