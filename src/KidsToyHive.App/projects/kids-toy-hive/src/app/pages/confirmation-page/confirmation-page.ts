// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, Query, ViewChild, OnInit, ElementRef } from '@angular/core';
import { Subject } from 'rxjs';
import { SalesOrderService } from '../../core';
import SignaturePad  from 'signature_pad';

@Component({
  standalone: true,
  templateUrl: './confirmation-page.html',
  styleUrls: ['./confirmation-page.scss'],
  selector: 'kth-confirmation-page'
})
export class ConfirmationPage implements OnDestroy, OnInit  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @ViewChild('signaturePad',{ static: true })
  public signaturePad:ElementRef;

  constructor(private readonly _salesOrderService: SalesOrderService) {

  }
  
  ngOnInit() {
    
    const signaturePad = new SignaturePad(this.signaturePad.nativeElement,{
      minWidth: 5,
      maxWidth: 10,
      penColor: 'rgb(0, 0, 0)'
  });

  
  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }


}

