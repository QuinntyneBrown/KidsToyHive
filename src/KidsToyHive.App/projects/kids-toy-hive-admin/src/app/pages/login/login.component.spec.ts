// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { of, throwError } from 'rxjs';
import { LoginComponent } from './login.component';
import { AuthService } from '../../core/services/auth.service';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;
  let snackBar: jasmine.SpyObj<MatSnackBar>;

  beforeEach(async () => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['login']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);
    const snackBarSpy = jasmine.createSpyObj('MatSnackBar', ['open']);

    await TestBed.configureTestingModule({
      imports: [
        LoginComponent,
        ReactiveFormsModule,
        HttpClientTestingModule,
        BrowserAnimationsModule
      ],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: MatSnackBar, useValue: snackBarSpy },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              queryParams: {}
            }
          }
        }
      ]
    }).compileComponents();

    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    snackBar = TestBed.inject(MatSnackBar) as jasmine.SpyObj<MatSnackBar>;

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize login form with empty values', () => {
    expect(component.loginForm).toBeDefined();
    expect(component.loginForm.get('email')?.value).toBe('');
    expect(component.loginForm.get('password')?.value).toBe('');
  });

  describe('Form Validation', () => {
    it('should be invalid when empty', () => {
      expect(component.loginForm.valid).toBe(false);
    });

    it('should validate email format', () => {
      const emailControl = component.loginForm.get('email');
      
      emailControl?.setValue('invalid-email');
      expect(emailControl?.hasError('email')).toBe(true);
      
      emailControl?.setValue('valid@email.com');
      expect(emailControl?.hasError('email')).toBe(false);
    });

    it('should require email', () => {
      const emailControl = component.loginForm.get('email');
      emailControl?.setValue('');
      expect(emailControl?.hasError('required')).toBe(true);
    });

    it('should require password', () => {
      const passwordControl = component.loginForm.get('password');
      passwordControl?.setValue('');
      expect(passwordControl?.hasError('required')).toBe(true);
    });

    it('should validate password minimum length', () => {
      const passwordControl = component.loginForm.get('password');
      
      passwordControl?.setValue('123');
      expect(passwordControl?.hasError('minlength')).toBe(true);
      
      passwordControl?.setValue('123456');
      expect(passwordControl?.hasError('minlength')).toBe(false);
    });

    it('should be valid with correct inputs', () => {
      component.loginForm.patchValue({
        email: 'test@example.com',
        password: 'password123'
      });
      expect(component.loginForm.valid).toBe(true);
    });
  });

  describe('onSubmit', () => {
    it('should not submit if form is invalid', () => {
      component.onSubmit();
      expect(authService.login).not.toHaveBeenCalled();
    });

    it('should call authService.login with form values', () => {
      const credentials = {
        email: 'admin@example.com',
        password: 'password123'
      };

      component.loginForm.patchValue(credentials);
      authService.login.and.returnValue(of({ accessToken: 'token' }));

      component.onSubmit();

      expect(authService.login).toHaveBeenCalledWith(credentials);
    });

    it('should navigate to returnUrl on successful login', () => {
      component.loginForm.patchValue({
        email: 'admin@example.com',
        password: 'password123'
      });
      component.returnUrl = '/dashboard';
      authService.login.and.returnValue(of({ accessToken: 'token' }));

      component.onSubmit();

      expect(router.navigateByUrl).toHaveBeenCalledWith('/dashboard');
    });

    it('should show success message on successful login', () => {
      component.loginForm.patchValue({
        email: 'admin@example.com',
        password: 'password123'
      });
      authService.login.and.returnValue(of({ accessToken: 'token' }));

      component.onSubmit();

      expect(snackBar.open).toHaveBeenCalledWith(
        'Login successful!',
        'Close',
        jasmine.objectContaining({
          duration: 3000,
          panelClass: ['success-snackbar']
        })
      );
    });

    it('should show error message on failed login', () => {
      component.loginForm.patchValue({
        email: 'admin@example.com',
        password: 'wrongpassword'
      });
      authService.login.and.returnValue(
        throwError(() => new Error('Invalid credentials'))
      );

      component.onSubmit();

      expect(component.isLoading).toBe(false);
      expect(snackBar.open).toHaveBeenCalledWith(
        'Invalid credentials',
        'Close',
        jasmine.objectContaining({
          duration: 5000,
          panelClass: ['error-snackbar']
        })
      );
    });

    it('should set isLoading during login', () => {
      component.loginForm.patchValue({
        email: 'admin@example.com',
        password: 'password123'
      });
      authService.login.and.returnValue(of({ accessToken: 'token' }));

      expect(component.isLoading).toBe(false);
      component.onSubmit();
      expect(component.isLoading).toBe(true);
    });
  });

  describe('togglePasswordVisibility', () => {
    it('should toggle hidePassword flag', () => {
      expect(component.hidePassword).toBe(true);
      
      component.togglePasswordVisibility();
      expect(component.hidePassword).toBe(false);
      
      component.togglePasswordVisibility();
      expect(component.hidePassword).toBe(true);
    });
  });

  describe('onForgotPassword', () => {
    it('should show snackbar message', () => {
      component.onForgotPassword();

      expect(snackBar.open).toHaveBeenCalledWith(
        'Password reset feature coming soon!',
        'Close',
        jasmine.objectContaining({
          duration: 3000
        })
      );
    });
  });

  describe('returnUrl from query params', () => {
    it('should use returnUrl from query params', () => {
      const activatedRoute = TestBed.inject(ActivatedRoute);
      activatedRoute.snapshot.queryParams = { returnUrl: '/admin' };

      const newFixture = TestBed.createComponent(LoginComponent);
      const newComponent = newFixture.componentInstance;
      newFixture.detectChanges();

      expect(newComponent.returnUrl).toBe('/admin');
    });

    it('should default to / when no returnUrl in query params', () => {
      expect(component.returnUrl).toBe('/');
    });
  });
});
