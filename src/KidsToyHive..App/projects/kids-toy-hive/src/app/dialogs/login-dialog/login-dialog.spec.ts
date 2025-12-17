// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { of } from 'rxjs';
import { LoginDialog } from './login-dialog';
import { AuthService } from '../../core/services/auth.service';

describe('LoginDialog', () => {
  let component: LoginDialog;
  let fixture: ComponentFixture<LoginDialog>;
  let authService: jasmine.SpyObj<AuthService>;
  let dialogRef: jasmine.SpyObj<MatDialogRef<LoginDialog>>;

  beforeEach(async () => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['tryToLogin']);
    const dialogRefSpy = jasmine.createSpyObj('MatDialogRef', ['close']);

    await TestBed.configureTestingModule({
      imports: [LoginDialog, ReactiveFormsModule],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: MatDialogRef, useValue: dialogRefSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginDialog);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    dialogRef = TestBed.inject(MatDialogRef) as jasmine.SpyObj<MatDialogRef<LoginDialog>>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a form with username and password controls', () => {
    expect(component.form.get('username')).toBeDefined();
    expect(component.form.get('password')).toBeDefined();
  });

  it('should require username and password', () => {
    const usernameControl = component.form.get('username');
    const passwordControl = component.form.get('password');

    expect(usernameControl?.hasError('required')).toBe(true);
    expect(passwordControl?.hasError('required')).toBe(true);
  });

  it('should mark form as valid when username and password are provided', () => {
    component.form.patchValue({
      username: 'testuser',
      password: 'testpass'
    });

    expect(component.form.valid).toBe(true);
  });

  it('should close dialog when close is called', () => {
    component.close();
    expect(dialogRef.close).toHaveBeenCalled();
  });

  it('should call authService.tryToLogin when form is valid', () => {
    authService.tryToLogin.and.returnValue(of({ token: 'test-token' }));

    component.form.patchValue({
      username: 'testuser',
      password: 'testpass'
    });

    component.tryToLogin();

    expect(authService.tryToLogin).toHaveBeenCalledWith({
      username: 'testuser',
      password: 'testpass'
    });
  });

  it('should not call authService when form is invalid', () => {
    component.tryToLogin();
    expect(authService.tryToLogin).not.toHaveBeenCalled();
  });

  it('should have onDestroy subject', () => {
    expect(component.onDestroy).toBeDefined();
  });

  it('should complete onDestroy on component destroy', () => {
    spyOn(component.onDestroy, 'next');
    component.ngOnDestroy();
    
    expect(component.onDestroy.next).toHaveBeenCalled();
  });
});
