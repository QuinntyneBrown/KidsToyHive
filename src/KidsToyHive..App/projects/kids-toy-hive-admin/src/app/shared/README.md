# Master-Detail Component Pattern

This directory contains a comprehensive, reusable master-detail CRUD pattern that serves as the foundation for all entity management pages in the KidsToyHive Admin application.

## Overview

The master-detail pattern provides a consistent user experience for managing entities with:
- **Split Layout**: List view on the left, detail form on the right
- **Responsive Design**: Adapts to mobile and desktop screens
- **CRUD Operations**: Create, Read, Update, Delete functionality
- **Search & Filter**: Real-time search with debouncing
- **State Management**: RxJS-based reactive state
- **Optimistic Updates**: Immediate UI updates for better UX
- **Error Handling**: Comprehensive error handling and user feedback

## Architecture

### Base Classes

#### `BaseMasterDetailComponent<T>`
Abstract base class that provides common CRUD functionality:
- Item list management
- Selection state
- Loading and error states
- Search/filter functionality
- Create, update, delete operations

**Key Features:**
- Generic type support (`T extends { id: string }`)
- RxJS-based state management
- Template method pattern for customization
- Optimistic updates
- Automatic drawer management

**Usage:**
```typescript
export class MyEntityComponent extends BaseMasterDetailComponent<MyEntity> {
  constructor() {
    super(inject(MatDialog), inject(MatSnackBar));
  }

  protected override getItems(): Observable<MyEntity[]> {
    return this.myEntityService.getAll();
  }

  protected override createItem(item: Partial<MyEntity>): Observable<MyEntity> {
    return this.myEntityService.create(item);
  }

  // ... implement other abstract methods
}
```

#### `BaseFormComponent<T>`
Abstract base class for entity detail forms:
- Form validation
- Dirty state tracking
- Save/cancel/delete actions
- Unsaved changes confirmation

**Usage:**
```typescript
export class MyEntityFormComponent extends BaseFormComponent<MyEntity> {
  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    // ... other fields
  });

  protected override getCreateTitle(): string {
    return 'Create My Entity';
  }

  protected override getEditTitle(): string {
    return `Edit: ${this.item?.name}`;
  }
}
```

#### `BaseHttpService<T>`
Abstract base class for HTTP services:
- Standard REST API methods
- Error handling
- Type-safe operations

**Usage:**
```typescript
@Injectable({ providedIn: 'root' })
export class MyEntityService extends BaseHttpService<MyEntity> {
  protected override get endpoint(): string {
    return 'api/my-entities';
  }

  constructor() {
    super(inject(HttpClient), environment.baseUrl);
  }
}
```

### UI Components

#### `MasterDetailComponent`
Reusable UI component for master-detail layout:
- Material Design components
- Virtual scrolling for performance
- Responsive layout with mat-drawer
- Custom item templates
- Search functionality
- Loading/error/empty states

**Features:**
- 400px list width on desktop, full width on mobile
- Right-side drawer for detail form
- FAB button for create action
- Real-time search with 300ms debounce
- Item count display
- Keyboard navigation support

#### `ConfirmDialogComponent`
Reusable confirmation dialog:
- Delete confirmations
- Unsaved changes warnings
- Customizable messages and actions

## File Structure

```
shared/
├── base/
│   ├── base-master-detail.component.ts    # Base master-detail component
│   └── base-form.component.ts             # Base form component
├── components/
│   └── master-detail.component.ts         # UI master-detail component
├── dialogs/
│   └── confirm-dialog.component.ts        # Confirmation dialog
├── services/
│   ├── base-http.service.ts              # Base HTTP service
│   └── base-http.service.spec.ts         # HTTP service tests
├── models/
│   └── confirm-dialog-data.ts            # Dialog data interface
└── index.ts                               # Public API
```

## Creating a New Entity Page

Follow these steps to create a new entity management page:

### 1. Create Entity Model
```typescript
export interface MyEntity {
  id: string;
  name: string;
  // ... other properties
}
```

### 2. Create Service
```typescript
@Injectable({ providedIn: 'root' })
export class MyEntityService extends BaseHttpService<MyEntity> {
  protected override get endpoint(): string {
    return 'api/my-entities';
  }

  constructor() {
    super(inject(HttpClient), environment.baseUrl);
  }
}
```

### 3. Create Form Component
```typescript
@Component({
  selector: 'app-my-entity-form',
  standalone: true,
  imports: [/* Material modules */],
  template: `
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <!-- Form toolbar with delete button -->
      <mat-toolbar color="primary">
        <span>{{ title }}</span>
        <button mat-icon-button (click)="onDelete()">
          <mat-icon>delete</mat-icon>
        </button>
      </mat-toolbar>

      <!-- Form fields -->
      <div class="form-content">
        <mat-form-field>
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>
      </div>

      <!-- Action buttons -->
      <div class="form-actions">
        <button mat-button (click)="onCancel()">Cancel</button>
        <button mat-raised-button color="primary" [disabled]="!canSave">Save</button>
      </div>
    </form>
  `
})
export class MyEntityFormComponent extends BaseFormComponent<MyEntity> {
  form: FormGroup = inject(FormBuilder).group({
    name: ['', Validators.required]
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
    return 'Create My Entity';
  }

  protected override getEditTitle(): string {
    return `Edit: ${this.item?.name}`;
  }
}
```

### 4. Create Page Component
```typescript
@Component({
  selector: 'app-my-entity',
  standalone: true,
  imports: [MasterDetailComponent, MyEntityFormComponent],
  template: `
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
        <div>{{ item.name }}</div>
      </ng-template>

      <!-- Form section -->
      <div form>
        <app-my-entity-form
          [item]="selectedItemSubject.value"
          [mode]="formMode()"
          [loading]="loadingSubject.value"
          (save)="onSave($event)"
          (delete)="onDelete($event)"
          (cancel)="onCancel()">
        </app-my-entity-form>
      </div>
    </app-master-detail>
  `
})
export class MyEntityComponent extends BaseMasterDetailComponent<MyEntity> {
  private readonly myEntityService = inject(MyEntityService);

  constructor() {
    super(inject(MatDialog), inject(MatSnackBar));
  }

  protected override getItems(): Observable<MyEntity[]> {
    return this.myEntityService.getAll();
  }

  protected override createItem(item: Partial<MyEntity>): Observable<MyEntity> {
    return this.myEntityService.create(item);
  }

  protected override updateItem(id: string, item: Partial<MyEntity>): Observable<MyEntity> {
    return this.myEntityService.update(id, item);
  }

  protected override deleteItem(id: string): Observable<void> {
    return this.myEntityService.delete(id);
  }
}
```

### 5. Add Route
```typescript
{
  path: 'my-entities',
  component: MyEntityComponent,
  canActivate: [authGuard]
}
```

## Demo Page

A complete working example is available at `/demo` route. This demonstrates:
- Full CRUD operations with mock data
- Custom list item template
- Form validation
- Search functionality
- Responsive layout
- All UI states (loading, error, empty)

To view the demo:
1. Start the dev server: `npm run start:admin`
2. Navigate to: `http://localhost:4200/demo`

## Testing

Unit tests are provided for:
- `BaseHttpService` - HTTP operations and error handling
- `ConfirmDialogComponent` - Dialog behavior and user interactions

To run tests:
```bash
npm test
```

## Best Practices

1. **Always extend base classes** - Don't create CRUD logic from scratch
2. **Use TypeScript generics** - Ensure type safety throughout
3. **Implement custom search** - Override `matchesSearchTerm()` for better search
4. **Provide custom templates** - Use `itemTemplate` for rich list items
5. **Handle errors gracefully** - Let base class handle common errors, override for specific cases
6. **Test your components** - Follow existing test patterns
7. **Keep forms simple** - One form component per entity
8. **Use reactive forms** - Better validation and state management

## Material Components Used

- `MatSidenavModule` - Drawer layout
- `MatListModule` - Item list
- `MatFormFieldModule` - Form inputs
- `MatButtonModule` - Buttons and FABs
- `MatIconModule` - Icons
- `MatDialogModule` - Dialogs
- `MatSnackBarModule` - Notifications
- `MatProgressSpinnerModule` - Loading indicators
- `ScrollingModule` - Virtual scrolling

## Future Enhancements

Potential improvements:
- Pagination support
- Sorting capabilities
- Advanced filtering
- Bulk operations
- Export functionality
- Drag-and-drop reordering
- Inline editing
- Column customization

## Support

For questions or issues with the master-detail pattern, please refer to:
- Demo page implementation (`pages/demo/`)
- Existing tests (`shared/**/*.spec.ts`)
- Angular Material documentation
