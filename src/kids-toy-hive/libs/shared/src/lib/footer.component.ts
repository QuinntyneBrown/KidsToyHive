import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
  selector: 'kth-footer'
})
export class FooterComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
