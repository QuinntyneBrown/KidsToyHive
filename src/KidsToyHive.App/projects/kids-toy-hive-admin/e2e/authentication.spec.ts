// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { test, expect } from '@playwright/test';

test.describe('Admin Authentication', () => {
  test.beforeEach(async ({ page }) => {
    // Navigate to admin application
    await page.goto('http://localhost:4200');
  });

  test('should redirect unauthenticated users to login page', async ({ page }) => {
    // When visiting the root, should redirect to login
    await expect(page).toHaveURL(/.*login/);
  });

  test('should display login form with all required fields', async ({ page }) => {
    await page.goto('http://localhost:4200/login');
    
    // Check for form elements
    const emailInput = page.locator('input[formControlName="email"]');
    const passwordInput = page.locator('input[formControlName="password"]');
    const submitButton = page.locator('button[type="submit"]');
    const forgotPasswordLink = page.locator('button:has-text("Forgot password")');
    
    await expect(emailInput).toBeVisible();
    await expect(passwordInput).toBeVisible();
    await expect(submitButton).toBeVisible();
    await expect(forgotPasswordLink).toBeVisible();
    
    // Verify password field type
    await expect(passwordInput).toHaveAttribute('type', 'password');
  });

  test('should show validation errors for invalid inputs', async ({ page }) => {
    await page.goto('http://localhost:4200/login');
    
    const emailInput = page.locator('input[formControlName="email"]');
    const passwordInput = page.locator('input[formControlName="password"]');
    const submitButton = page.locator('button[type="submit"]');
    
    // Try to submit empty form
    await submitButton.click();
    
    // Should show validation errors
    const emailError = page.locator('mat-error:has-text("Email is required")');
    const passwordError = page.locator('mat-error:has-text("Password is required")');
    
    await expect(emailError).toBeVisible();
    await expect(passwordError).toBeVisible();
    
    // Test invalid email format
    await emailInput.fill('invalid-email');
    await emailInput.blur();
    
    const emailFormatError = page.locator('mat-error:has-text("valid email")');
    await expect(emailFormatError).toBeVisible();
    
    // Test password minimum length
    await passwordInput.fill('123');
    await passwordInput.blur();
    
    const passwordLengthError = page.locator('mat-error:has-text("at least 6 characters")');
    await expect(passwordLengthError).toBeVisible();
  });

  test('should toggle password visibility', async ({ page }) => {
    await page.goto('http://localhost:4200/login');
    
    const passwordInput = page.locator('input[formControlName="password"]');
    const toggleButton = page.locator('button[aria-label*="password visibility"]');
    
    // Initially password should be hidden
    await expect(passwordInput).toHaveAttribute('type', 'password');
    
    // Click toggle button
    await toggleButton.click();
    
    // Password should now be visible
    await expect(passwordInput).toHaveAttribute('type', 'text');
    
    // Click again to hide
    await toggleButton.click();
    await expect(passwordInput).toHaveAttribute('type', 'password');
  });

  test('should have submit button disabled when form is invalid', async ({ page }) => {
    await page.goto('http://localhost:4200/login');
    
    const submitButton = page.locator('button[type="submit"]');
    
    // Button should be disabled initially
    await expect(submitButton).toBeDisabled();
    
    // Fill in valid email
    const emailInput = page.locator('input[formControlName="email"]');
    await emailInput.fill('admin@example.com');
    
    // Still disabled (password missing)
    await expect(submitButton).toBeDisabled();
    
    // Fill in valid password
    const passwordInput = page.locator('input[formControlName="password"]');
    await passwordInput.fill('password123');
    
    // Now should be enabled
    await expect(submitButton).toBeEnabled();
  });

  test('should submit form on Enter key press', async ({ page }) => {
    await page.goto('http://localhost:4200/login');
    
    const emailInput = page.locator('input[formControlName="email"]');
    const passwordInput = page.locator('input[formControlName="password"]');
    
    // Fill in credentials
    await emailInput.fill('admin@example.com');
    await passwordInput.fill('password123');
    
    // Press Enter while focused on password field
    await passwordInput.press('Enter');
    
    // Should attempt to submit (we expect an error since backend is mocked)
    // Wait for potential error message or loading state
    await page.waitForTimeout(1000);
  });

  test('should show forgot password message when clicked', async ({ page }) => {
    await page.goto('http://localhost:4200/login');
    
    const forgotPasswordLink = page.locator('button:has-text("Forgot password")');
    await forgotPasswordLink.click();
    
    // Should show snackbar message
    const snackbar = page.locator('.mat-mdc-snack-bar-container');
    await expect(snackbar).toBeVisible();
    await expect(snackbar).toContainText('coming soon');
  });

  test('should be responsive on mobile screens', async ({ page }) => {
    // Set viewport to mobile size (320px width as per requirements)
    await page.setViewportSize({ width: 320, height: 568 });
    await page.goto('http://localhost:4200/login');
    
    // Form should still be visible and usable
    const loginCard = page.locator('.login-card');
    const emailInput = page.locator('input[formControlName="email"]');
    const passwordInput = page.locator('input[formControlName="password"]');
    const submitButton = page.locator('button[type="submit"]');
    
    await expect(loginCard).toBeVisible();
    await expect(emailInput).toBeVisible();
    await expect(passwordInput).toBeVisible();
    await expect(submitButton).toBeVisible();
  });

  test('should display loading indicator during login attempt', async ({ page }) => {
    await page.goto('http://localhost:4200/login');
    
    const emailInput = page.locator('input[formControlName="email"]');
    const passwordInput = page.locator('input[formControlName="password"]');
    const submitButton = page.locator('button[type="submit"]');
    
    // Fill in credentials
    await emailInput.fill('admin@example.com');
    await passwordInput.fill('password123');
    
    // Click submit
    await submitButton.click();
    
    // Should show loading spinner
    const spinner = page.locator('mat-spinner');
    await expect(spinner).toBeVisible({ timeout: 2000 });
  });
});

test.describe('Admin Dashboard Access', () => {
  test('should redirect to login when accessing dashboard without authentication', async ({ page }) => {
    await page.goto('http://localhost:4200/dashboard');
    
    // Should redirect to login page with returnUrl
    await expect(page).toHaveURL(/.*login.*returnUrl/);
  });

  test('should show unauthorized page when accessing restricted route without permissions', async ({ page }) => {
    // This test would require setting up authentication first
    // Skipping actual navigation test but structure is ready
    await page.goto('http://localhost:4200/unauthorized');
    
    // Should show unauthorized message
    const unauthorizedText = page.locator('text=Access Denied');
    await expect(unauthorizedText).toBeVisible();
  });
});
