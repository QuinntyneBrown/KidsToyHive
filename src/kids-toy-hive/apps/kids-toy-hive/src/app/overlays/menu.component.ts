import { Component, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
  selector: 'kth-menu'
})
export class MenuComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Output()
  public signIn:EventEmitter<any> = new EventEmitter();

  @Output()
  public signOut: EventEmitter<any> = new EventEmitter();

  @Output()
  public navigateToUrl: EventEmitter<any> = new EventEmitter();

  @Input()
  public isAuthenticated:boolean;

  constructor(

  ) {

  }

  handleMyProfileClick() {
    this.navigateToUrl.emit('/myprofile');
  }
  
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
