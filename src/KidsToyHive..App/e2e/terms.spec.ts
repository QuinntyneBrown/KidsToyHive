import { test, expect } from '@playwright/test';

test.describe('Terms and Conditions Page', () => {
  test('should navigate to terms and conditions page', async ({ page }) => {
    await page.goto('/legal');
    await expect(page).toHaveURL(/\/legal/);
  });

  test('should display terms page content', async ({ page }) => {
    await page.goto('/legal');
    
    const termsPage = page.locator('kth-terms-and-conditions-page');
    await expect(termsPage).toBeVisible({ timeout: 5000 });
  });

  test('should load HTML content', async ({ page }) => {
    await page.goto('/legal');
    
    // The terms page loads HTML content dynamically
    await page.waitForLoadState('networkidle');
    
    const content = page.locator('.html-content, [innerHTML]');
    await expect(content.first()).toBeAttached();
  });
});
