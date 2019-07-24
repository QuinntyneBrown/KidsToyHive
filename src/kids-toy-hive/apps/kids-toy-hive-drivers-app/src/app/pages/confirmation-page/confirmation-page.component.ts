import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './confirmation-page.component.html',
  styleUrls: ['./confirmation-page.component.css'],
  selector: 'confirmation-page-confirmation-page'
})
export class ConfirmationPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
