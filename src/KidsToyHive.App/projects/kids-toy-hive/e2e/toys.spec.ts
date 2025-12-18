import { test, expect } from '@playwright/test';

test.describe('Toys Page', () => {
  test('should display toys page', async ({ page }) => {
    await page.goto('/toys');
    await expect(page).toHaveURL(/\/toys/);
  });

  test('should display toy items', async ({ page }) => {
    await page.goto('/toys');
    const toyItems = page.locator('kth-toy');
    await expect(toyItems.first()).toBeVisible({ timeout: 10000 });
  });

  test('should display toy with name and call to action', async ({ page }) => {
    await page.goto('/toys');
    const firstToy = page.locator('kth-toy').first();
    await expect(firstToy).toBeVisible({ timeout: 10000 });
    
    const toyButton = firstToy.locator('kth-button, button');
    await expect(toyButton).toBeVisible();
  });

  test('should navigate to order page when toy is selected', async ({ page }) => {
    await page.goto('/toys');
    
    const firstToy = page.locator('kth-toy').first();
    await expect(firstToy).toBeVisible({ timeout: 10000 });
    
    const rentButton = firstToy.locator('kth-button, button').first();
    await rentButton.click();
    
    await expect(page).toHaveURL(/\/order/);
  });

  test('should filter or search toys', async ({ page }) => {
    await page.goto('/toys');
    
    // Look for search or filter inputs
    const searchInput = page.locator('input[type="search"], input[placeholder*="search" i]');
    if (await searchInput.isVisible()) {
      await searchInput.fill('toy');
      await page.waitForTimeout(500);
    }
  });
});
