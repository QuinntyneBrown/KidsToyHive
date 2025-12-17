// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { async, TestBed } from '@angular/core/testing';
import { DomainModule } from '../../core';

describe('DomainModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [DomainModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(DomainModule).toBeDefined();
  });
});

