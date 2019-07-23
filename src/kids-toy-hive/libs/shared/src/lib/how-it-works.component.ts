import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './how-it-works.component.html',
  styleUrls: ['./how-it-works.component.css'],
  selector: 'kth-how-it-works'
})
export class HowItWorksComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
