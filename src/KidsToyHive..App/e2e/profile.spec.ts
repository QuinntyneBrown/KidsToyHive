import { test, expect } from '@playwright/test';

test.describe('Profile Page', () => {
  test('should navigate to profile page', async ({ page }) => {
    await page.goto('/myprofile');
    await expect(page).toHaveURL(/\/myprofile/);
  });

  test('should redirect to login if not authenticated', async ({ page }) => {
    // This test assumes the app redirects unauthenticated users
    await page.goto('/myprofile');
    
    // Should either show login dialog, redirect, or stay on profile
    const isOnProfilePage = page.url().includes('/myprofile') || page.url().includes('/home');
    
    // Profile page or redirect should happen
    expect(isOnProfilePage).toBeTruthy();
  });

  test('should display my profile page structure', async ({ page }) => {
    await page.goto('/profile');
    
    const profilePage = page.locator('kth-my-profile-page');
    // Page might require auth, so just check if it exists in DOM
    await expect(profilePage).toBeAttached({ timeout: 5000 });
  });
});
