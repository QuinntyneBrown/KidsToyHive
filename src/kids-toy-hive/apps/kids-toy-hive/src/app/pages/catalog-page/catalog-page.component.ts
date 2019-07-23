import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './catalog-page.component.html',
  styleUrls: ['./catalog-page.component.css'],
  selector: 'kth-catalog-page'
})
export class CatalogPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
