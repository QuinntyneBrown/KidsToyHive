// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { getGreeting } from '../support/app.po';

describe('kids-toy-hive-drivers-app', () => {
  beforeEach(() => cy.visit('/'));

  it('should display welcome message', () => {
    getGreeting().contains('Welcome to kids-toy-hive-drivers-app!');
  });
});

