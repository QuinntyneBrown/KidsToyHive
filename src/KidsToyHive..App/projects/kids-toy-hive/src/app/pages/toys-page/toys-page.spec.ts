// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { ToysPage } from './toys-page';
import { ProductService, Product } from '../../core';
import { baseUrl, LocalStorageService } from '../../core';

describe('ToysPage', () => {
  let component: ToysPage;
  let fixture: ComponentFixture<ToysPage>;
  let productService: jasmine.SpyObj<ProductService>;
  let localStorageService: jasmine.SpyObj<LocalStorageService>;
  let router: jasmine.SpyObj<Router>;

  const mockProducts: Product[] = [
    {
      productId: 1,
      name: 'Test Toy',
      productImages: [{ url: '/images/toy1.jpg' }]
    } as Product
  ];

  beforeEach(async () => {
    const productServiceSpy = jasmine.createSpyObj('ProductService', ['get']);
    const localStorageServiceSpy = jasmine.createSpyObj('LocalStorageService', ['put', 'get']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    await TestBed.configureTestingModule({
      imports: [ToysPage],
      providers: [
        { provide: ProductService, useValue: productServiceSpy },
        { provide: LocalStorageService, useValue: localStorageServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: baseUrl, useValue: 'http://localhost:5000' }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ToysPage);
    component = fixture.componentInstance;
    productService = TestBed.inject(ProductService) as jasmine.SpyObj<ProductService>;
    localStorageService = TestBed.inject(LocalStorageService) as jasmine.SpyObj<LocalStorageService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    productService.get.and.returnValue(of(mockProducts));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load toys on init', () => {
    component.ngOnInit();
    
    expect(productService.get).toHaveBeenCalled();
    component.toys$.subscribe(toys => {
      expect(toys).toEqual(mockProducts);
    });
  });

  it('should navigate to order page and store product id on Get It Now click', () => {
    const testToy = mockProducts[0];
    
    component.onGetItNowClick(testToy);

    expect(localStorageService.put).toHaveBeenCalledWith({
      name: 'productId',
      value: testToy.productId
    });
    expect(router.navigateByUrl).toHaveBeenCalledWith('/order');
  });

  it('should build correct image URL', () => {
    const testToy = mockProducts[0];
    const imageUrl = component.buildImageUrl(testToy);

    expect(imageUrl).toBe('http://localhost:5000/images/toy1.jpg');
  });

  it('should have toys$ observable', () => {
    expect(component.toys$).toBeUndefined();
    component.ngOnInit();
    expect(component.toys$).toBeDefined();
  });

  it('should call onDestroy on component destroy', () => {
    spyOn(component.onDestroy, 'next');
    component.ngOnDestroy();
    
    expect(component.onDestroy.next).toHaveBeenCalled();
  });
});
