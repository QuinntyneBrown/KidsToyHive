import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  templateUrl: './create-booking-section.component.html',
  styleUrls: ['./create-booking-section.component.css'],
  selector: 'kth-create-booking-section'
})
export class CreateBookingSectionComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    private readonly _router: Router
  ) {

  }
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
