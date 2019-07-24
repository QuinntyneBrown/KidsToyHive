import { Component, OnDestroy, Query, ViewChild, OnInit, ElementRef } from '@angular/core';
import { Subject } from 'rxjs';
import { SalesOrderService } from '@kids-toy-hive/domain';
import SignaturePad  from 'signature_pad';

@Component({
  templateUrl: './confirmation-page.component.html',
  styleUrls: ['./confirmation-page.component.css'],
  selector: 'kth-confirmation-page'
})
export class ConfirmationPageComponent implements OnDestroy, OnInit  { 
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
