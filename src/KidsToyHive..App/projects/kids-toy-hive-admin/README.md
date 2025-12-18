# KidsToyHive Admin Application

## Overview

The KidsToyHive Admin application is a secure, role-based admin portal built with Angular 21 and Angular Material 21. It provides comprehensive authentication and authorization features for managing the KidsToyHive platform.

## Features

### Authentication & Authorization
- **JWT-based authentication** with automatic token management
- **Role-based access control** (RBAC) with four default roles:
  - SuperAdmin: Full system access
  - Admin: User and content management
  - Manager: Product and order management
  - Viewer: Read-only access
- **Permission-based UI control** using structural directives
- **Secure HTTP interceptor** for automatic token injection
- **Route guards** protecting sensitive pages
- **Token expiration handling** with automatic refresh

### User Interface
- **Responsive login page** supporting screens from 320px+
- **Material Design 3** components throughout
- **Password visibility toggle** for better UX
- **Loading indicators** for async operations
- **Snackbar notifications** for user feedback
- **Dashboard** with role-based feature visibility

## Getting Started

### Prerequisites
- Node.js 20.x or higher
- npm 10.x or higher
- Angular CLI 21.x

### Installation

```bash
# Install dependencies
cd src/KidsToyHive..App
npm install
```

### Development

```bash
# Start the development server
npm run start:admin

# The app will be available at http://localhost:4200
```

### Building

```bash
# Build for production
ng build kids-toy-hive-admin --configuration=production

# Output will be in dist/kids-toy-hive-admin
```

### Testing

```bash
# Run unit tests
npm test -- --project=kids-toy-hive-admin

# Run E2E tests
npm run e2e
```

## Architecture

### Project Structure

```
projects/kids-toy-hive-admin/
├── src/
│   ├── app/
│   │   ├── core/
│   │   │   ├── directives/
│   │   │   │   └── has-permission.directive.ts
│   │   │   ├── enums/
│   │   │   │   ├── permission.enum.ts
│   │   │   │   └── role.enum.ts
│   │   │   ├── guards/
│   │   │   │   ├── auth.guard.ts
│   │   │   │   └── role.guard.ts
│   │   │   ├── interceptors/
│   │   │   │   └── auth.interceptor.ts
│   │   │   ├── models/
│   │   │   │   ├── login-request.model.ts
│   │   │   │   ├── login-response.model.ts
│   │   │   │   └── user.model.ts
│   │   │   └── services/
│   │   │       ├── auth.service.ts
│   │   │       └── permission.service.ts
│   │   ├── pages/
│   │   │   ├── dashboard/
│   │   │   ├── login/
│   │   │   └── unauthorized/
│   │   ├── app.config.ts
│   │   ├── app.routes.ts
│   │   └── app.ts
│   ├── environments/
│   │   ├── environment.ts
│   │   └── environment.prod.ts
│   └── main.ts
├── e2e/
│   └── authentication.spec.ts
└── tsconfig.app.json
```

### Core Services

#### AuthService
Handles all authentication operations:
- `login(credentials)` - Authenticate user
- `logout()` - Clear session and redirect
- `refreshToken()` - Renew access token
- `getCurrentUser()` - Fetch user profile
- `isAuthenticated()` - Check auth status
- `isAuthenticated$` - Observable auth state

#### PermissionService
Manages permissions and roles:
- `hasRole(role)` - Check user role
- `hasPermission(permission)` - Check permission
- `hasMinimumRole(role)` - Check role hierarchy
- `getUserPermissions()` - Get all permissions

### Guards

#### authGuard
Protects routes requiring authentication. Redirects to login with return URL if not authenticated.

```typescript
{
  path: 'dashboard',
  component: DashboardComponent,
  canActivate: [authGuard]
}
```

#### roleGuard
Restricts access based on user roles. Redirects to unauthorized page if user lacks required role.

```typescript
{
  path: 'admin',
  component: AdminComponent,
  canActivate: [authGuard, roleGuard],
  data: { roles: [Role.Admin, Role.SuperAdmin] }
}
```

### Directives

#### *hasPermission
Conditionally displays elements based on user permissions:

```html
<button *hasPermission="Permission.ManageUsers">
  Manage Users
</button>

<!-- Multiple permissions (any) -->
<div *hasPermission="[Permission.ManageUsers, Permission.ViewReports]">
  Content
</div>
```

### HTTP Interceptor

The `authInterceptor` automatically:
- Adds Authorization header with JWT token
- Excludes auth endpoints (/api/auth/*)
- Handles 401 errors and redirects to login
- Works with concurrent requests

## API Integration

### Required Backend Endpoints

```typescript
POST /api/auth/login
{
  email: string;
  password: string;
}
Response: {
  accessToken: string;
  refreshToken?: string;
  expiresIn?: number;
}

POST /api/auth/refresh
{
  refreshToken: string;
}
Response: {
  accessToken: string;
  refreshToken?: string;
}

POST /api/auth/logout
No body
Response: Empty

GET /api/auth/me
Response: {
  id: string;
  email: string;
  name: string;
  roles: string[];
  permissions?: string[];
}
```

### JWT Token Format

The JWT token should contain:
```json
{
  "sub": "user-id",
  "email": "user@example.com",
  "name": "User Name",
  "roles": ["Admin"],
  "permissions": ["ManageUsers"],
  "exp": 1234567890,
  "iat": 1234567890
}
```

## Security Considerations

### Token Storage
- Access tokens stored in localStorage
- Refresh tokens stored separately
- Tokens cleared on logout
- Automatic cleanup on expiration

### Input Validation
- Email format validation
- Password minimum length (6 characters)
- All form inputs sanitized
- XSS protection via Angular

### Best Practices
- Use HTTPS in production
- Implement rate limiting on backend
- Set appropriate token expiration times
- Validate tokens server-side
- Use secure, HTTP-only cookies for refresh tokens (if available)

## Configuration

### Environment Variables

```typescript
// environment.ts (development)
export const environment = {
  production: false,
  baseUrl: 'http://localhost:5000/'
};

// environment.prod.ts (production)
export const environment = {
  production: true,
  baseUrl: '/' // Use relative path in production
};
```

## Customization

### Adding New Roles

1. Add role to enum:
```typescript
// core/enums/role.enum.ts
export enum Role {
  SuperAdmin = 'SuperAdmin',
  Admin = 'Admin',
  Manager = 'Manager',
  Viewer = 'Viewer',
  CustomRole = 'CustomRole' // New role
}
```

2. Update role hierarchy in PermissionService
3. Add role permissions mapping

### Adding New Permissions

1. Add permission to enum:
```typescript
// core/enums/permission.enum.ts
export enum Permission {
  // ...existing permissions
  CustomPermission = 'CustomPermission'
}
```

2. Map permission to roles in PermissionService

## Testing

### Unit Tests
- All services have >80% coverage
- Tests use Jasmine/Jest framework
- Mock HTTP calls and dependencies
- Test authentication flows and edge cases

### E2E Tests
- Playwright-based end-to-end tests
- Test complete authentication flow
- Verify responsive design
- Test form validation and error handling

## Troubleshooting

### Common Issues

**Build Errors**
- Ensure all dependencies are installed: `npm install`
- Clear Angular cache: `rm -rf .angular/cache`
- Rebuild: `ng build kids-toy-hive-admin`

**Login Fails**
- Check API endpoint URL in environment
- Verify network connectivity
- Check browser console for errors
- Ensure backend is running

**Token Expired**
- Tokens automatically refresh if refresh token is valid
- Check token expiration time in JWT
- Implement refresh token endpoint on backend

## Contributing

When contributing to the authentication system:
1. Follow existing code patterns
2. Add unit tests for new features
3. Update this README if adding new features
4. Ensure responsive design works on 320px+ screens
5. Run `ng lint` before committing

## License

Copyright (c) Quinntyne Brown. All Rights Reserved.
Licensed under the MIT License. See License.txt in the project root for license information.
