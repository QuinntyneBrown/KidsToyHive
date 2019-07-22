import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  selector: 'kth-home-page'
})
export class HomePageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
