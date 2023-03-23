import { Component, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css'],
  selector: 'kth-button'
})
export class ButtonComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Input()
  public text: string;

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
