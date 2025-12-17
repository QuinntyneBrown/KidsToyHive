import { test, expect } from '@playwright/test';

test.describe('Authentication', () => {
  test('should open login dialog', async ({ page }) => {
    await page.goto('/');
    
    // Open menu
    const menuButton = page.locator('kth-hamburger-button');
    await menuButton.click();
    
    // Click sign in
    const signInButton = page.getByRole('button', { name: /sign in/i });
    await signInButton.click();
    
    // Verify login dialog is visible
    const loginDialog = page.locator('kth-login-dialog');
    await expect(loginDialog).toBeVisible();
  });

  test('should show validation error for empty login form', async ({ page }) => {
    await page.goto('/');
    
    // Open menu and login dialog
    const menuButton = page.locator('kth-hamburger-button');
    await menuButton.click();
    const signInButton = page.getByRole('button', { name: /sign in/i });
    await signInButton.click();
    
    // Try to submit empty form
    const submitButton = page.locator('kth-login-dialog button[type="submit"]');
    await submitButton.click();
    
    // Should show error message
    const errorMessage = page.locator('kth-login-dialog .error, kth-login-dialog [class*="error"]');
    await expect(errorMessage).toBeVisible();
  });

  test('should close login dialog', async ({ page }) => {
    await page.goto('/');
    
    // Open login dialog
    const menuButton = page.locator('kth-hamburger-button');
    await menuButton.click();
    const signInButton = page.getByRole('button', { name: /sign in/i });
    await signInButton.click();
    
    // Close dialog
    const closeButton = page.locator('kth-login-dialog button[mat-dialog-close], kth-login-dialog .close');
    await closeButton.click();
    
    // Verify dialog is closed
    const loginDialog = page.locator('kth-login-dialog');
    await expect(loginDialog).not.toBeVisible();
  });

  test('should have username and password fields', async ({ page }) => {
    await page.goto('/');
    
    const menuButton = page.locator('kth-hamburger-button');
    await menuButton.click();
    const signInButton = page.getByRole('button', { name: /sign in/i });
    await signInButton.click();
    
    const usernameInput = page.locator('input[formControlName="username"], input[name="username"]');
    const passwordInput = page.locator('input[formControlName="password"], input[name="password"]');
    
    await expect(usernameInput).toBeVisible();
    await expect(passwordInput).toBeVisible();
    await expect(passwordInput).toHaveAttribute('type', 'password');
  });
});
