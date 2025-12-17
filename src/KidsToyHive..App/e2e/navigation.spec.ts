import { test, expect } from '@playwright/test';

test.describe('Navigation', () => {
  test('should navigate to about page', async ({ page }) => {
    await page.goto('/');
    await page.goto('/about');
    await expect(page).toHaveURL(/\/about/);
  });

  test('should navigate to toys page', async ({ page }) => {
    await page.goto('/');
    await page.goto('/toys');
    await expect(page).toHaveURL(/\/toys/);
  });

  test('should navigate to terms and conditions', async ({ page }) => {
    await page.goto('/');
    await page.goto('/legal');
    await expect(page).toHaveURL(/\/legal/);
  });

  test('should navigate back to home from logo click', async ({ page }) => {
    await page.goto('/toys');
    const logo = page.locator('kth-header img, kth-header [role="img"]').first();
    await logo.click();
    await expect(page).toHaveURL('/');
  });

  test('should show mobile menu on small screens', async ({ page, viewport }) => {
    await page.setViewportSize({ width: 375, height: 667 });
    await page.goto('/');
    const menuButton = page.locator('kth-hamburger-button');
    await expect(menuButton).toBeVisible();
  });
});
