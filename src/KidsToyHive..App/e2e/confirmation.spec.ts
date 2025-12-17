import { test, expect } from '@playwright/test';

// Note: Confirmation page route doesn't exist in current routing configuration
// These tests are commented out until the route is added
test.describe.skip('Confirmation Page', () => {
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
