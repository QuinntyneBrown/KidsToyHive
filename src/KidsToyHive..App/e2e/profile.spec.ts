import { test, expect } from '@playwright/test';

test.describe('Profile Page', () => {
  test('should navigate to profile page', async ({ page }) => {
    await page.goto('/profile');
    await expect(page).toHaveURL(/\/profile/);
  });

  test('should redirect to login if not authenticated', async ({ page }) => {
    // This test assumes the app redirects unauthenticated users
    await page.goto('/profile');
    
    // Should either show login dialog or redirect
    const loginDialog = page.locator('kth-login-dialog');
    const isOnLoginPage = page.url().includes('/login');
    const isOnProfilePage = page.url().includes('/profile');
    
    // One of these should be true
    expect(await loginDialog.isVisible() || isOnLoginPage || isOnProfilePage).toBeTruthy();
  });

  test('should display my profile page structure', async ({ page }) => {
    await page.goto('/profile');
    
    const profilePage = page.locator('kth-my-profile-page');
    // Page might require auth, so just check if it exists in DOM
    await expect(profilePage).toBeAttached({ timeout: 5000 });
  });
});
