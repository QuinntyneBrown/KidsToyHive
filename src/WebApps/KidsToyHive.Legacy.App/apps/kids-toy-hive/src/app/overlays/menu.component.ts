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
  public isMobile: boolean;

  @Input()
  public isAuthenticated:boolean;

  handleMyProfileClick() {
    this.navigateToUrl.emit('/myprofile');
  }
  
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
