import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './about-page.component.html',
  styleUrls: ['./about-page.component.css'],
  selector: 'kth-about-page'
})
export class AboutPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
