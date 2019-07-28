import { Component, OnDestroy, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './toy.component.html',
  styleUrls: ['./toy.component.css'],
  selector: 'kth-toy'
})
export class ToyComponent implements OnDestroy, OnInit  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Input()
  public callToActionText:string;

  @Input()
  public imageUrl:string;

  @Input()
  public title: string;
  
  @Output()
  public callToActionClick: EventEmitter<any> = new EventEmitter();

  @Input()
  public baseUrl:string;

  public ngOnInit() {

  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
