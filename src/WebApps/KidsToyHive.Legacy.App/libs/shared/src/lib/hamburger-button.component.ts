import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './hamburger-button.component.html',
  styleUrls: ['./hamburger-button.component.css'],
  selector: 'kth-hamburger-button'
})
export class HamburgerButtonComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
