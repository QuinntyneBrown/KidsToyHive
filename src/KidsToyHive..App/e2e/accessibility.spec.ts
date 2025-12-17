import { test, expect } from '@playwright/test';

test.describe('Accessibility', () => {
  test('should have proper page titles', async ({ page }) => {
    await page.goto('/');
    await expect(page).toHaveTitle(/.+/);
    
    await page.goto('/toys');
    await expect(page).toHaveTitle(/.+/);
    
    await page.goto('/about');
    await expect(page).toHaveTitle(/.+/);
  });

  test('should have keyboard navigable buttons', async ({ page }) => {
    await page.goto('/');
    
    // Tab through the page
    await page.keyboard.press('Tab');
    const focusedElement = await page.evaluate(() => document.activeElement?.tagName);
    expect(focusedElement).toBeTruthy();
  });

  test('should have alt text for images', async ({ page }) => {
    await page.goto('/');
    
    const images = page.locator('img');
    const count = await images.count();
    
    for (let i = 0; i < Math.min(count, 5); i++) {
      const img = images.nth(i);
      if (await img.isVisible()) {
        const alt = await img.getAttribute('alt');
        // Alt can be empty string for decorative images, but should be present
        expect(alt).not.toBeNull();
      }
    }
  });

  test('should have proper form labels', async ({ page }) => {
    await page.goto('/order/step/1');
    
    const inputs = page.locator('input[type="text"], input[type="email"]');
    const count = await inputs.count();
    
    for (let i = 0; i < Math.min(count, 3); i++) {
      const input = inputs.nth(i);
      const id = await input.getAttribute('id');
      if (id) {
        const label = page.locator(`label[for="${id}"]`);
        const hasLabel = await label.count() > 0;
        const hasAriaLabel = await input.getAttribute('aria-label');
        const hasPlaceholder = await input.getAttribute('placeholder');
        
        // Should have at least one form of labeling
        expect(hasLabel || hasAriaLabel || hasPlaceholder).toBeTruthy();
      }
    }
  });

  test('should support Enter key for form submission', async ({ page }) => {
    await page.goto('/order/step/1');
    
    const firstInput = page.locator('input').first();
    await firstInput.click();
    await page.keyboard.press('Enter');
    
    // Form should attempt validation or submission
    await page.waitForTimeout(500);
  });
});
