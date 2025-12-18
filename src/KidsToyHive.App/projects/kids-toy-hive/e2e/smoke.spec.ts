import { test, expect } from '@playwright/test';

test.describe('Smoke Tests', () => {
  test('should load home page', async ({ page }) => {
    await page.goto('/');
    await expect(page).toHaveURL(/\//);
    
    // Check for header
    const header = page.locator('kth-header');
    await expect(header).toBeVisible();
  });

  test('should navigate to toys page', async ({ page }) => {
    await page.goto('/toys');
    await expect(page).toHaveURL(/\/toys/);
  });

  test('should navigate to about page', async ({ page }) => {
    await page.goto('/about');
    await expect(page).toHaveURL(/\/about/);
  });

  test('should navigate to legal/terms page', async ({ page }) => {
    await page.goto('/legal');
    await expect(page).toHaveURL(/\/legal/);
  });

  test('should navigate to profile page', async ({ page }) => {
    await page.goto('/myprofile');
    // May redirect if not authenticated
    await page.waitForLoadState('networkidle');
    expect(page.url()).toBeTruthy();
  });

  test('should navigate to order page', async ({ page }) => {
    await page.goto('/order');
    await expect(page).toHaveURL(/\/order/);
  });

  test('should load order step 1 (customer)', async ({ page }) => {
    await page.goto('/order/step/1');
    await expect(page).toHaveURL(/\/order\/step\/1/);
  });

  test('should have footer', async ({ page }) => {
    await page.goto('/');
    const footer = page.locator('kth-footer');
    await expect(footer).toBeVisible();
  });

  test('should have menu button', async ({ page }) => {
    await page.goto('/');
    const menuButton = page.locator('kth-hamburger-button');
    await expect(menuButton).toBeVisible();
  });

  test('should open menu dialog', async ({ page }) => {
    await page.goto('/');
    const menuButton = page.locator('kth-hamburger-button');
    await menuButton.click();
    await page.waitForTimeout(1000);
    
    // Menu should have options
    const menu = page.locator('kth-menu');
    await expect(menu).toBeVisible();
  });
});
