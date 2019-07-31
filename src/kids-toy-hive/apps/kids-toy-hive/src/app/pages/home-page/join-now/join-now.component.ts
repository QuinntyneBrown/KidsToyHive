import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './join-now.component.html',
  styleUrls: ['./join-now.component.css'],
  selector: 'kth-join-now'
})
export class JoinNowComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
