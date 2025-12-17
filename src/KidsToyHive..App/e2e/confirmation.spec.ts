import { test, expect } from '@playwright/test';

test.describe('Confirmation Page', () => {
  test('should navigate to confirmation page', async ({ page }) => {
    await page.goto('/confirmation');
    await expect(page).toHaveURL(/\/confirmation/);
  });

  test('should display confirmation page content', async ({ page }) => {
    await page.goto('/confirmation');
    
    const confirmationPage = page.locator('kth-confirmation-page');
    await expect(confirmationPage).toBeVisible({ timeout: 5000 });
  });

  test('should show booking confirmation message', async ({ page }) => {
    await page.goto('/confirmation');
    
    // Look for confirmation text
    const confirmationText = page.getByText(/confirm|thank you|success/i);
    await expect(confirmationText.first()).toBeVisible({ timeout: 5000 });
  });
});
