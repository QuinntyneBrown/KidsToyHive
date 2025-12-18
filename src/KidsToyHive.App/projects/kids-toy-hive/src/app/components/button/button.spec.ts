// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Button } from './button';

describe('Button', () => {
  let component: Button;
  let fixture: ComponentFixture<Button>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Button]
    }).compileComponents();

    fixture = TestBed.createComponent(Button);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have text input property', () => {
    const testText = 'Click Me';
    component.text = testText;
    fixture.detectChanges();
    
    expect(component.text).toBe(testText);
  });

  it('should render button text', () => {
    const testText = 'Submit';
    component.text = testText;
    fixture.detectChanges();
    
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain(testText);
  });

  it('should have onDestroy subject', () => {
    expect(component.onDestroy).toBeDefined();
  });

  it('should call onDestroy on component destroy', () => {
    jest.spyOn(component.onDestroy, 'next');
    component.ngOnDestroy();
    
    expect(component.onDestroy.next).toHaveBeenCalled();
  });
});
