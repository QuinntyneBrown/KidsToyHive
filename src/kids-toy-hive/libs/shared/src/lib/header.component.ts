import { Component, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  selector: 'kth-header'
})
export class HeaderComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Output()
  public menuClick: EventEmitter<any> = new EventEmitter();

  @Output()
  public logoClick: EventEmitter<any> = new EventEmitter();
  
  @Input()
  public imageUrl:string;
  
  @Input()
  public hideNavigation:boolean;

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
