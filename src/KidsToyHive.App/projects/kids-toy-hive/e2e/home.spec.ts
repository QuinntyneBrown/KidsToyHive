import { test, expect } from '@playwright/test';

test.describe('Home Page', () => {
  test('should load home page successfully', async ({ page }) => {
    await page.goto('/');
    await expect(page).toHaveTitle(/Toy Rental|Kids Toy Hive/i);
  });

  test('should display header with logo', async ({ page }) => {
    await page.goto('/');
    const header = page.locator('kth-header');
    await expect(header).toBeVisible();
  });

  test('should display how it works section', async ({ page }) => {
    await page.goto('/');
    const howItWorks = page.locator('kth-how-it-works');
    await expect(howItWorks).toBeVisible();
  });

  test('should have main content', async ({ page }) => {
    await page.goto('/');
    // Check for any links or buttons on home page
    const links = page.locator('a, button');
    await expect(links.first()).toBeVisible();
  });

  test('should display footer', async ({ page }) => {
    await page.goto('/');
    const footer = page.locator('kth-footer');
    await expect(footer).toBeVisible();
  });

  test('should open menu dialog when hamburger is clicked', async ({ page }) => {
    await page.goto('/');
    const menuButton = page.locator('kth-hamburger-button');
    await menuButton.click();
    const menuDialog = page.locator('kth-menu-dialog');
    await expect(menuDialog).toBeVisible();
  });
});
