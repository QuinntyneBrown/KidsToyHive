import { getGreeting } from '../support/app.po';

describe('kids-toy-hive-admin', () => {
  beforeEach(() => cy.visit('/'));

  it('should display welcome message', () => {
    getGreeting().contains('Welcome to kids-toy-hive-admin!');
  });
});
