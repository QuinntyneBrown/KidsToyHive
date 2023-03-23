// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { async, TestBed } from '@angular/core/testing';
import { FeaturesSecurityModule } from './features-security.module';

describe('FeaturesSecurityModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FeaturesSecurityModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(FeaturesSecurityModule).toBeDefined();
  });
});

