import { test, expect } from '@playwright/test';

test.describe('Order Flow', () => {
  test('should display order page', async ({ page }) => {
    await page.goto('/order');
    await expect(page).toHaveURL(/\/order/);
  });

  test('should show order steps navigation', async ({ page }) => {
    await page.goto('/order');
    
    // Should have router outlet for multi-step process
    const routerOutlet = page.locator('router-outlet');
    await expect(routerOutlet).toBeAttached();
  });

  test('should navigate to customer information step', async ({ page }) => {
    await page.goto('/order/step/1');
    await expect(page).toHaveURL(/\/order\/step\/1/);
  });

  test('should show customer form on step 1', async ({ page }) => {
    await page.goto('/order/step/1');
    
    const customerForm = page.locator('kth-create-customer-section form, form[formGroup]');
    await expect(customerForm).toBeVisible({ timeout: 5000 });
  });

  test('should require customer details', async ({ page }) => {
    await page.goto('/order/step/1');
    
    const firstNameInput = page.locator('input[formControlName="firstName"], input[name="firstName"]');
    const lastNameInput = page.locator('input[formControlName="lastName"], input[name="lastName"]');
    const emailInput = page.locator('input[formControlName="email"], input[name="email"]');
    
    await expect(firstNameInput).toBeVisible({ timeout: 5000 });
    await expect(lastNameInput).toBeVisible();
    await expect(emailInput).toBeVisible();
  });

  test('should navigate to booking details step', async ({ page }) => {
    await page.goto('/order/step/2');
    await expect(page).toHaveURL(/\/order\/step\/2/);
  });

  test('should show booking form on step 2', async ({ page }) => {
    await page.goto('/order/step/2');
    
    const bookingForm = page.locator('kth-create-booking-section form, form[formGroup]');
    await expect(bookingForm).toBeVisible({ timeout: 5000 });
  });

  test('should have date picker for booking', async ({ page }) => {
    await page.goto('/order/step/2');
    
    const dateInput = page.locator('input[formControlName="date"], input[type="date"], mat-datepicker-toggle');
    await expect(dateInput).toBeVisible({ timeout: 5000 });
  });

  test('should have time slot selection', async ({ page }) => {
    await page.goto('/order/step/2');
    
    const timeSlotSelect = page.locator('select[formControlName="bookingTimeSlot"], mat-select[formControlName="bookingTimeSlot"]');
    await expect(timeSlotSelect).toBeVisible({ timeout: 5000 });
  });

  test('should navigate to payment step', async ({ page }) => {
    await page.goto('/order/step/3');
    await expect(page).toHaveURL(/\/order\/step\/3/);
  });

  test('should show payment form on step 3', async ({ page }) => {
    await page.goto('/order/step/3');
    
    const paymentForm = page.locator('kth-process-booking-payment form, form[formGroup]');
    await expect(paymentForm).toBeVisible({ timeout: 5000 });
  });

  test('should require payment card details', async ({ page }) => {
    await page.goto('/order/step/3');
    
    const cardNumberInput = page.locator('input[formControlName="number"], input[name="cardNumber"]');
    const expMonthInput = page.locator('input[formControlName="expMonth"], select[formControlName="expMonth"]');
    const cvcInput = page.locator('input[formControlName="cvc"], input[name="cvc"]');
    
    await expect(cardNumberInput).toBeVisible({ timeout: 5000 });
    await expect(expMonthInput).toBeVisible();
    await expect(cvcInput).toBeVisible();
  });

  test('should display order summary', async ({ page }) => {
    await page.goto('/order');
    
    const orderSummary = page.locator('kth-your-order');
    await expect(orderSummary).toBeVisible({ timeout: 5000 });
  });
});
