import { Component, OnDestroy, Input } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  selector: 'kth-header'
})
export class HeaderComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Input()
  public imageUrl:string;
  
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
