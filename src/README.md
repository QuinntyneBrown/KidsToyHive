# KidsToyHive

A comprehensive toy rental and management platform built with Angular (Nx workspace) and .NET Core.

## Project Structure

This is a monorepo containing both frontend and backend applications:

```
├── KidsToyHive.App/          # Angular Nx workspace
│   ├── projects/
│   │   ├── kids-toy-hive/              # Main customer application
│   │   ├── kids-toy-hive-admin/        # Admin dashboard
│   │   └── kids-toy-hive-drivers/      # Drivers application
│   └── libs/                           # Shared Angular libraries
│       ├── core/
│       ├── domain/
│       ├── features/
│       └── shared/
│
├── KidsToyHive.Api/          # ASP.NET Core Web API
├── KidsToyHive.Core/         # Core business logic and domain
├── KidsToyHive.Domain/       # Domain models and entities
└── KidsToyHive.Infrastructure/ # Data access and infrastructure
```

## Prerequisites

### Frontend
- Node.js (v12 or higher)
- npm or yarn
- Angular CLI
- Nx CLI

### Backend
- .NET Core SDK (3.1 or higher)
- SQL Server or PostgreSQL
- Visual Studio 2019+ or VS Code

## Getting Started

### Backend Setup

1. Navigate to the API project:
   ```powershell
   cd KidsToyHive.Api
   ```

2. Restore dependencies:
   ```powershell
   dotnet restore
   ```

3. Update database connection string in `appsettings.Development.json`

4. Run database migrations:
   ```powershell
   dotnet ef database update
   ```

5. Run the API:
   ```powershell
   dotnet run
   ```

   The API will be available at `https://localhost:5001`

### Frontend Setup

1. Navigate to the Angular workspace:
   ```powershell
   cd KidsToyHive.App
   ```

2. Install dependencies:
   ```powershell
   npm install
   ```

3. Start the desired application:
   
   **Main Application:**
   ```powershell
   npm start
   # or
   ng serve kids-toy-hive
   ```
   
   **Admin Dashboard:**
   ```powershell
   npm run start:admin
   ```
   
   **Drivers App:**
   ```powershell
   npm run start:drivers
   ```

   Applications will be available at `http://localhost:4200`

## Development

### Running Tests

**Backend:**
```powershell
dotnet test
```

**Frontend:**
```powershell
cd KidsToyHive.App
npm test
```

### Running E2E Tests
```powershell
cd KidsToyHive.App
npm run e2e
```

### Code Quality

**Linting:**
```powershell
cd KidsToyHive.App
npm run lint
```

**Format Check:**
```powershell
npm run format:check
```

### Nx Workspace Commands

**View dependency graph:**
```powershell
npm run dep-graph
```

**Affected commands:**
```powershell
npm run affected:test    # Test affected projects
npm run affected:build   # Build affected projects
npm run affected:lint    # Lint affected projects
```

## Building for Production

### Backend
```powershell
cd KidsToyHive.Api
dotnet publish -c Release -o ./publish
```

### Frontend
```powershell
cd KidsToyHive.App
ng build --prod --project=kids-toy-hive
```

Build artifacts will be stored in the `dist/` directory.

## Architecture

### Backend
- **Clean Architecture** with clear separation of concerns
- **CQRS Pattern** using MediatR
- **Entity Framework Core** for data access
- **ASP.NET Core Identity** for authentication
- **Health Checks** for monitoring
- **Middleware** for exception handling and logging

### Frontend
- **Nx Monorepo** for workspace management
- **Angular 8+** with Material Design
- **SignalR** for real-time communication
- **RxJS** for reactive programming
- **Jest** for unit testing
- **Cypress** for E2E testing

## Key Features

- Customer toy rental management
- Admin dashboard for inventory and orders
- Driver application for deliveries
- Real-time updates with SignalR
- Booking and shipment tracking
- Digital asset management
- Survey and feedback system
- Professional service provider integration

## Database Reset

To reset the development database:
```powershell
cd KidsToyHive.Api
.\resetdb.ps1
```

## Contributing

1. Create a feature branch from `main`
2. Make your changes
3. Run tests and linting
4. Submit a pull request

## License

MIT License - Copyright (c) Quinntyne Brown. All Rights Reserved.

## Further Documentation

- [Nx Documentation](https://nx.dev)
- [Angular Documentation](https://angular.io)
- [.NET Core Documentation](https://docs.microsoft.com/dotnet/core)
