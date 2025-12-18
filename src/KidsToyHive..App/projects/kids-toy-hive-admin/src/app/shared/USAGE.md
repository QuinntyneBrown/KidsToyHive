# Master-Detail Pattern - Quick Start Guide

This guide will walk you through creating a new entity management page using the master-detail pattern in 10 minutes.

## Prerequisites

Before starting, ensure you have:
- Angular CLI installed
- Access to the kids-toy-hive-admin project
- Basic understanding of Angular, TypeScript, and RxJS

## Step-by-Step Tutorial

### Step 1: Create Your Entity Model

Create a new file for your entity type:

**File:** `src/app/pages/[entity-name]/models/[entity-name].ts`

```typescript
export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  category: string;
  inStock: boolean;
  createdDate: Date;
  updatedDate?: Date;
}
```

### Step 2: Create the HTTP Service

Create a service that extends `BaseHttpService`:

**File:** `src/app/pages/[entity-name]/services/[entity-name].service.ts`

```typescript
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseHttpService } from '../../../shared/services/base-http.service';
import { Product } from '../models/product';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseHttpService<Product> {
  protected override get endpoint(): string {
    return 'api/products';  // Your API endpoint
  }

  constructor() {
    super(inject(HttpClient), environment.baseUrl);
  }
}
```

**That's it!** Your service now has:
- `getAll(): Observable<Product[]>`
- `getById(id: string): Observable<Product>`
- `create(product: Partial<Product>): Observable<Product>`
- `update(id: string, product: Partial<Product>): Observable<Product>`
- `delete(id: string): Observable<void>`

### Step 3: Create the Form Component

Create a form component that extends `BaseFormComponent`:

**File:** `src/app/pages/[entity-name]/[entity-name]-form.component.ts`

```typescript
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
import { MatDialog } from '@angular/material/dialog';
import { BaseFormComponent } from '../../shared/base/base-form.component';
import { Product } from './models/product';

@Component({
  selector: 'app-product-form',
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
    MatToolbarModule
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
          <mat-label>Product Name</mat-label>
          <input matInput formControlName="name" required>
          <mat-error *ngIf="form.get('name')?.hasError('required')">
            Name is required
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Price</mat-label>
          <input matInput type="number" formControlName="price" required>
          <mat-error *ngIf="form.get('price')?.hasError('required')">
            Price is required
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Category</mat-label>
          <mat-select formControlName="category" required>
            <mat-option value="Electronics">Electronics</mat-option>
            <mat-option value="Clothing">Clothing</mat-option>
            <mat-option value="Books">Books</mat-option>
          </mat-select>
        </mat-form-field>

        <div class="toggle-field">
          <mat-slide-toggle formControlName="inStock">
            In Stock
          </mat-slide-toggle>
        </div>
      </div>

      <div class="form-actions">
        <button mat-button type="button" (click)="onCancel()" [disabled]="loading">
          Cancel
        </button>
        <button mat-raised-button color="primary" type="submit" [disabled]="!canSave">
          Save
        </button>
      </div>
    </form>
  `,
  styles: [`
    :host { display: block; height: 100%; }
    form { display: flex; flex-direction: column; height: 100%; }
    .spacer { flex: 1 1 auto; }
    .form-content { 
      flex: 1; 
      padding: 24px; 
      overflow-y: auto; 
      display: flex; 
      flex-direction: column; 
      gap: 16px; 
    }
    mat-form-field { width: 100%; }
    .toggle-field { padding: 8px 0; }
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
export class ProductFormComponent extends BaseFormComponent<Product> implements OnInit {
  private readonly fb = inject(FormBuilder);

  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    price: [0, [Validators.required, Validators.min(0)]],
    category: ['', Validators.required],
    inStock: [true]
  });

  constructor() {
    super(inject(MatDialog));
  }

  override ngOnInit(): void {
    if (this.item) {
      this.form.patchValue(this.item);
    }
  }

  protected override getCreateTitle(): string {
    return 'Create Product';
  }

  protected override getEditTitle(): string {
    return `Edit: ${this.item?.name || 'Product'}`;
  }

  protected override getDeleteMessage(): string {
    return `Are you sure you want to delete "${this.item?.name}"? This action cannot be undone.`;
  }
}
```

### Step 4: Create the Page Component

Create the main page component that extends `BaseMasterDetailComponent`:

**File:** `src/app/pages/[entity-name]/[entity-name].component.ts`

```typescript
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseMasterDetailComponent } from '../../shared/base/base-master-detail.component';
import { MasterDetailComponent } from '../../shared/components/master-detail.component';
import { ProductFormComponent } from './product-form.component';
import { Product } from './models/product';
import { ProductService } from './services/product.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [
    CommonModule,
    MasterDetailComponent,
    ProductFormComponent
  ],
  template: `
    <div class="page-container">
      <h1>Products</h1>
      
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
        
        <!-- Optional: Custom list item template -->
        <ng-template #itemTemplate let-item>
          <div class="custom-item">
            <div class="item-header">
              <strong>{{ item.name }}</strong>
              <span class="price">\${{ item.price }}</span>
            </div>
            <div class="item-description">{{ item.description }}</div>
            <div class="item-meta">
              <span class="category">{{ item.category }}</span>
              <span [class.in-stock]="item.inStock" [class.out-of-stock]="!item.inStock">
                {{ item.inStock ? 'In Stock' : 'Out of Stock' }}
              </span>
            </div>
          </div>
        </ng-template>

        <!-- Form section -->
        <div form>
          <app-product-form
            [item]="selectedItemSubject.value"
            [mode]="formMode()"
            [loading]="loadingSubject.value"
            (save)="onSave($event)"
            (delete)="onDelete($event)"
            (cancel)="onCancel()">
          </app-product-form>
        </div>
      </app-master-detail>
    </div>
  `,
  styles: [`
    .page-container { height: 100vh; display: flex; flex-direction: column; }
    h1 { margin: 0; padding: 16px 24px; font-size: 28px; }
    app-master-detail { flex: 1; overflow: hidden; }
    .custom-item { display: flex; flex-direction: column; gap: 4px; padding: 8px 16px; width: 100%; }
    .item-header { display: flex; justify-content: space-between; align-items: center; }
    .price { font-weight: bold; color: #1976d2; }
    .item-description { font-size: 14px; color: rgba(0, 0, 0, 0.6); }
    .item-meta { display: flex; justify-content: space-between; font-size: 12px; }
    .in-stock { color: #4caf50; }
    .out-of-stock { color: #f44336; }
  `]
})
export class ProductsComponent extends BaseMasterDetailComponent<Product> {
  private readonly productService = inject(ProductService);

  constructor() {
    super(inject(MatDialog), inject(MatSnackBar));
  }

  protected override getItems(): Observable<Product[]> {
    return this.productService.getAll();
  }

  protected override createItem(item: Partial<Product>): Observable<Product> {
    return this.productService.create(item);
  }

  protected override updateItem(id: string, item: Partial<Product>): Observable<Product> {
    return this.productService.update(id, item);
  }

  protected override deleteItem(id: string): Observable<void> {
    return this.productService.delete(id);
  }

  /**
   * Optional: Custom search logic
   */
  protected override matchesSearchTerm(item: Product, searchTerm: string): boolean {
    return (
      item.name.toLowerCase().includes(searchTerm) ||
      item.description.toLowerCase().includes(searchTerm) ||
      item.category.toLowerCase().includes(searchTerm)
    );
  }
}
```

### Step 5: Add the Route

Add your new page to the routing configuration:

**File:** `src/app/app.routes.ts`

```typescript
import { ProductsComponent } from './pages/products/products.component';

export const routes: Routes = [
  // ... existing routes
  {
    path: 'products',
    component: ProductsComponent,
    canActivate: [authGuard]
  }
];
```

### Step 6: Test Your New Page

1. Start the development server:
```bash
npm run start:admin
```

2. Navigate to: `http://localhost:4200/products`

3. Test the functionality:
   - ✅ List view displays products
   - ✅ Search filters items
   - ✅ Click item to view/edit
   - ✅ Click FAB to create new item
   - ✅ Save changes
   - ✅ Delete items with confirmation

## What You Get Automatically

By using the master-detail pattern, you automatically get:

### UI Features
- ✅ Responsive split layout (list + detail)
- ✅ Material Design components
- ✅ Loading indicators
- ✅ Error states
- ✅ Empty states
- ✅ Mobile-friendly design

### Functionality
- ✅ CRUD operations (Create, Read, Update, Delete)
- ✅ Search/filter with debouncing
- ✅ Optimistic updates
- ✅ Form validation
- ✅ Dirty state tracking
- ✅ Delete confirmations
- ✅ Success/error notifications
- ✅ Keyboard navigation

### Developer Experience
- ✅ Type-safe with TypeScript generics
- ✅ RxJS-based reactive state
- ✅ Minimal boilerplate code
- ✅ Consistent patterns across all pages
- ✅ Easy to test
- ✅ Well-documented

## Customization Options

### Custom List Item Template

```html
<ng-template #itemTemplate let-item>
  <div class="my-custom-item">
    <!-- Your custom layout -->
  </div>
</ng-template>
```

### Custom Search Logic

```typescript
protected override matchesSearchTerm(item: MyEntity, searchTerm: string): boolean {
  // Your custom search logic
  return item.field1.includes(searchTerm) || item.field2.includes(searchTerm);
}
```

### Custom Validation

```typescript
form: FormGroup = this.fb.group({
  name: ['', [Validators.required, Validators.minLength(3)]],
  email: ['', [Validators.required, Validators.email]],
  age: ['', [Validators.required, Validators.min(0), Validators.max(120)]]
});
```

### Additional Form Fields

Add any Material form fields:
- `mat-input` - Text inputs
- `mat-select` - Dropdowns
- `mat-checkbox` - Checkboxes
- `mat-slide-toggle` - Toggle switches
- `mat-datepicker` - Date pickers
- `mat-radio-group` - Radio buttons
- `mat-autocomplete` - Autocomplete

## Common Issues & Solutions

### Issue: My service returns different data structure
**Solution:** Map the data in your service:
```typescript
override getAll(): Observable<Product[]> {
  return this.http.get<ApiResponse>(`${this.baseUrl}${this.endpoint}`)
    .pipe(map(response => response.data));
}
```

### Issue: I need authentication headers
**Solution:** Use an HTTP interceptor (already configured in the app)

### Issue: I want pagination
**Solution:** Override `getAll()` to add pagination parameters:
```typescript
getAll(page: number = 1, pageSize: number = 50): Observable<Product[]> {
  return this.http.get<Product[]>(`${this.baseUrl}${this.endpoint}?page=${page}&pageSize=${pageSize}`);
}
```

### Issue: Form is too complex
**Solution:** Split into multiple form components and use `<ng-container>` to include them

## Next Steps

1. Review the [demo page](../pages/demo/) for a complete working example
2. Check the [main README](./README.md) for advanced features
3. Create your first entity page following this guide
4. Customize as needed for your specific requirements

## Support

For questions or issues:
- Review the demo implementation
- Check the base class implementations
- Consult the comprehensive README
- Ask your team members

Happy coding! 🚀
