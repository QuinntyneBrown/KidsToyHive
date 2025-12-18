# KidsToyHive Angular Application

Modern Angular 21 application for toy rental services, featuring standalone components, Angular Material design, and comprehensive end-to-end testing.

## Technology Stack

- **Angular 21.0** - Standalone components architecture
- **Angular Material 21.0** - Material Design UI components
- **TypeScript** - Type-safe development
- **RxJS** - Reactive programming
- **Playwright** - End-to-end testing
- **Jest** - Unit testing
- **SCSS** - Styling

## Project Structure

```
projects/kids-toy-hive/
├── src/
│   ├── app/
│   │   ├── app.component.ts          # Root component
│   │   ├── app.routes.ts             # Application routing
│   │   ├── components/               # Reusable UI components
│   │   ├── core/                     # Core services and models
│   │   ├── dialogs/                  # Material dialogs
│   │   ├── pages/                    # Page components
│   │   └── sections/                 # Multi-step sections
│   ├── assets/                       # Images, fonts, etc.
│   └── styles/                       # Global styles
├── e2e/                              # Playwright e2e tests
└── playwright.config.ts              # Playwright configuration
```

## Getting Started

### Prerequisites

- Node.js (v18 or higher)
- npm or yarn

### Installation

```bash
# Install dependencies
npm install

# Note: Use --legacy-peer-deps if you encounter peer dependency issues
npm install --legacy-peer-deps
```

### Development Server

```bash
# Start development server
npm run start
# or
ng serve kids-toy-hive
```

Navigate to `http://localhost:4200/`. The app will automatically reload when you change source files.

### Build

```bash
# Development build
npm run build
# or
ng build kids-toy-hive

# Production build
ng build kids-toy-hive --configuration production
```

Build artifacts are stored in the `dist/` directory.

## Testing

### Unit Tests

```bash
# Run unit tests
npm test
# or
ng test kids-toy-hive
```

Tests run via [Jest](https://jestjs.io).

### End-to-End Tests

Comprehensive Playwright tests covering all application behaviors:

```bash
# Run all e2e tests (headless)
npm run e2e

# Run tests with UI mode (interactive)
npm run e2e:ui

# Run tests with browser visible
npm run e2e:headed

# Debug tests
npm run e2e:debug

# View test report
npm run e2e:report
```

#### E2E Test Coverage

- **Home Page** - Navigation, CTAs, menu interactions
- **Authentication** - Login/logout flows, form validation
- **Product Browsing** - Toy listing, filtering, selection
- **Order Flow** - Multi-step booking process:
  - Step 1: Customer information
  - Step 2: Booking details (date/time selection)
  - Step 3: Payment processing
- **Profile** - User profile and booking history
- **Static Pages** - About, Terms & Conditions
- **Responsive Design** - Mobile, tablet, desktop viewports
- **Accessibility** - Keyboard navigation, labels, ARIA

## Code Scaffolding

```bash
# Generate a new standalone component
ng generate component my-component --project=kids-toy-hive

# Generate a service
ng generate service my-service --project=kids-toy-hive

# Generate a dialog
ng generate component dialogs/my-dialog --project=kids-toy-hive
```

## Architecture

### Standalone Components

This application uses Angular's standalone component architecture (no NgModules). Components directly import their dependencies:

```typescript
@Component({
  selector: 'kth-my-component',
  standalone: true,
  imports: [CommonModule, MatButtonModule, RouterModule],
  templateUrl: './my-component.html'
})
export class MyComponent { }
```

### Routing

Routes are defined in [app.routes.ts](projects/kids-toy-hive/src/app/app.routes.ts) using the new functional router configuration.

### Services

Core services are located in `src/app/core/services/`:
- **AuthService** - Authentication and user management
- **BookingService** - Booking operations
- **ProductService** - Product catalog
- **PaymentService** - Payment processing

### Dialogs

Material dialogs replace custom overlay services:
- **MenuDialog** - Navigation menu
- **LoginDialog** - User authentication

## Features

### Customer-Facing Features

- Browse toy catalog with images and descriptions
- Multi-step booking process with form validation
- Date and time slot selection
- Secure payment processing
- User profile and booking history
- Responsive design for all devices

### Technical Features

- Standalone components (no NgModules)
- Angular Material design system
- Reactive forms with validation
- HTTP interceptors for API communication
- Route guards for protected pages
- Lazy loading for performance
- Comprehensive e2e test coverage

## Scripts Reference

```bash
npm run start              # Start dev server
npm run build              # Build project
npm test                   # Run unit tests
npm run lint               # Lint code
npm run e2e                # Run e2e tests
npm run e2e:ui             # Run e2e tests in UI mode
npm run e2e:headed         # Run e2e tests with browser visible
npm run e2e:debug          # Debug e2e tests
npm run e2e:report         # View e2e test report
```

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Mobile browsers (iOS Safari, Chrome for Android)

## Contributing

1. Create a feature branch
2. Make your changes
3. Run tests: `npm test` and `npm run e2e`
4. Ensure build passes: `npm run build`
5. Submit a pull request

## Learn More

- [Angular Documentation](https://angular.dev)
- [Angular Material](https://material.angular.io)
- [Playwright](https://playwright.dev)
- [RxJS](https://rxjs.dev)
