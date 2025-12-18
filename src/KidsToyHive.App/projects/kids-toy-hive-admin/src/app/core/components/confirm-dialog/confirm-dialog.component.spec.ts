// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmDialogComponent, ConfirmDialogData } from './confirm-dialog.component';

describe('ConfirmDialogComponent', () => {
  let component: ConfirmDialogComponent;
  let fixture: ComponentFixture<ConfirmDialogComponent>;
  let mockDialogRef: jasmine.SpyObj<MatDialogRef<ConfirmDialogComponent>>;
  const mockData: ConfirmDialogData = {
    title: 'Test Title',
    message: 'Test Message',
    confirmText: 'Yes',
    cancelText: 'No',
    confirmColor: 'warn'
  };

  beforeEach(async () => {
    mockDialogRef = jasmine.createSpyObj('MatDialogRef', ['close']);

    await TestBed.configureTestingModule({
      imports: [
        ConfirmDialogComponent,
        MatDialogModule,
        MatButtonModule
      ],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: mockData },
        { provide: MatDialogRef, useValue: mockDialogRef }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ConfirmDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display the title', () => {
    const compiled = fixture.nativeElement;
    const title = compiled.querySelector('h2');
    expect(title.textContent).toContain(mockData.title);
  });

  it('should display the message', () => {
    const compiled = fixture.nativeElement;
    const message = compiled.querySelector('p');
    expect(message.textContent).toContain(mockData.message);
  });

  it('should close with true when confirm is clicked', () => {
    component.onConfirm();
    expect(mockDialogRef.close).toHaveBeenCalledWith(true);
  });

  it('should close with false when cancel is clicked', () => {
    component.onCancel();
    expect(mockDialogRef.close).toHaveBeenCalledWith(false);
  });

  it('should use default text when not provided', async () => {
    const defaultData: ConfirmDialogData = {
      title: 'Test',
      message: 'Test'
    };

    TestBed.resetTestingModule();
    await TestBed.configureTestingModule({
      imports: [ConfirmDialogComponent],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: defaultData },
        { provide: MatDialogRef, useValue: mockDialogRef }
      ]
    }).compileComponents();

    const newFixture = TestBed.createComponent(ConfirmDialogComponent);
    newFixture.detectChanges();
    
    const compiled = newFixture.nativeElement;
    const buttons = compiled.querySelectorAll('button');
    expect(buttons[0].textContent).toContain('Cancel');
    expect(buttons[1].textContent).toContain('Confirm');
  });
});
