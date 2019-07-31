import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './testimonials.component.html',
  styleUrls: ['./testimonials.component.css'],
  selector: 'kth-testimonials'
})
export class TestimonialsComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
