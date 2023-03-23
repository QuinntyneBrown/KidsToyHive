import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { DashboardService } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.css'],
  selector: 'kth-dashboard-page'
})
export class DashboardPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(
    private readonly _dashboardService: DashboardService
  ) {

  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
