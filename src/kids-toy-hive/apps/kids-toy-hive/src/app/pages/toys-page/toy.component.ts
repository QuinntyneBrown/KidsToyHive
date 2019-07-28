import { Component, OnDestroy, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { Product } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './toy.component.html',
  styleUrls: ['./toy.component.css'],
  selector: 'kth-toy'
})
export class ToyComponent implements OnDestroy, OnInit  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Input()
  public toy:Product;

  @Output()
  public getItNowClick: EventEmitter<any> = new EventEmitter();

  @Input()
  public baseUrl:string;

  public ngOnInit() {

  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
