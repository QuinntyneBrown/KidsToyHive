import { test, expect } from '@playwright/test';

test.describe('Home Page', () => {
  test('should load home page successfully', async ({ page }) => {
    await page.goto('/');
    await expect(page).toHaveTitle(/KidsToyHive/i);
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

  test('should have explore now button', async ({ page }) => {
    await page.goto('/');
    const exploreButton = page.getByRole('button', { name: /explore now/i });
    await expect(exploreButton).toBeVisible();
  });

  test('should navigate to toys page when explore now is clicked', async ({ page }) => {
    await page.goto('/');
    const exploreButton = page.getByRole('button', { name: /explore now/i });
    await exploreButton.click();
    await expect(page).toHaveURL(/\/toys/);
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
