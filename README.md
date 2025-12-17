# KidsToyHive

Complete E-Commerce application to rent products (toys) as well as manage the logistics of delivering and picking up the products (toys). Multi-tenanted.

## Project Structure

This is a monorepo containing:

- **Angular Frontend** (`src/KidsToyHive..App/projects/kids-toy-hive`) - Customer-facing web application
- **.NET Backend** (`src/KidsToyHive.Api`) - RESTful API
- **Core Domain** (`src/KidsToyHive.Core`) - Business logic and domain models
- **Infrastructure** (`src/KidsToyHive.Infrastructure`) - Data access and external services
- **Domain** (`src/KidsToyHive.Domain`) - Domain entities

## Frontend Application

### Technology Stack

- Angular (Standalone Components)
- Angular Material
- TypeScript
- SCSS
- RxJS

### Getting Started

1. Navigate to the Angular app directory:
   ```bash
   cd src/KidsToyHive..App
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   ng serve kids-toy-hive
   ```

4. Open your browser to `http://localhost:4200`

### Build for Production

```bash
ng build kids-toy-hive --configuration=production
```

### Key Features

- Product browsing and rental
- Customer registration and authentication
- Booking management with date/time selection
- Payment processing
- Profile management
- Responsive design for mobile and desktop

## Backend API

### Technology Stack

- ASP.NET Core
- Entity Framework Core
- MediatR (CQRS pattern)
- SQL Server

### Running the API

1. Navigate to the API directory:
   ```bash
   cd src/KidsToyHive.Api
   ```

2. Update database connection string in `appsettings.Development.json`

3. Run the API:
   ```bash
   dotnet run
   ```

### Database Migrations

Reset database:
```powershell
.\resetdb.ps1
```

## Testing

- Frontend tests use Jest
- Backend tests use xUnit

Run frontend tests:
```bash
cd src/KidsToyHive..App
ng test kids-toy-hive
```

Run backend tests:
```bash
dotnet test
```

## Architecture

The application follows Clean Architecture principles with clear separation of concerns:

- **Presentation Layer**: Angular SPA
- **Application Layer**: API Controllers, MediatR Handlers
- **Domain Layer**: Business logic, entities, and domain services
- **Infrastructure Layer**: Data persistence, external services

## License

Licensed under the MIT License. See LICENSE file for details.



