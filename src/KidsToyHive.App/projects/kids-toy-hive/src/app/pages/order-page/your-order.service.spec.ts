// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { TestBed } from '@angular/core/testing';
import { YourOrderService } from './your-order.service';

describe('YourOrderService', () => {
  let service: YourOrderService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        YourOrderService,
        { provide: 'baseUrl', useValue: 'http://localhost' }
      ]
    });

    service = TestBed.inject(YourOrderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should be a service', () => {
    expect(service).toBeInstanceOf(YourOrderService);
  });
});
