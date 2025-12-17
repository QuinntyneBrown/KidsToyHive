import { test, expect } from '@playwright/test';

test.describe('About Page', () => {
  test('should navigate to about page', async ({ page }) => {
    await page.goto('/about');
    await expect(page).toHaveURL(/\/about/);
  });

  test('should display about page content', async ({ page }) => {
    await page.goto('/about');
    
    const aboutPage = page.locator('kth-about-page');
    await expect(aboutPage).toBeVisible({ timeout: 5000 });
  });

  test('should load HTML content', async ({ page }) => {
    await page.goto('/about');
    
    // The about page loads HTML content dynamically
    await page.waitForLoadState('networkidle');
    
    const content = page.locator('.html-content, .custom, [innerHTML]');
    await expect(content.first()).toBeAttached();
  });
});
