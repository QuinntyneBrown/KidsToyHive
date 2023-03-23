// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component } from "@angular/core";
import { Subject, BehaviorSubject } from "rxjs";
import { FormGroup, FormControl } from "@angular/forms";
import { OverlayRefWrapper } from "../core/overlay-ref-wrapper";
import { Dashboard } from "./dashboard.model";
import { map, switchMap, tap, takeUntil } from "rxjs/operators";
import { OverlayRefWrapper } from '@dashboards/shared';
import { Dashboard } from '@dashboards/domain';

@Component({
  templateUrl: "./add-dashboard-card.component.html",
  styleUrls: ["./add-dashboard-card.component.css"],
  selector: "dashboards-add-dashboard-card",
  host: { 'class': 'mat-typography' }
})
export class AddDashboardCardComponent { 
  constructor(
    private _dashboardService: DashboardService,
    private _overlay: OverlayRefWrapper) { }

  ngOnInit() {
    if (this.dashboardId)
      this._dashboardService.getById(this.dashboardId)
        .pipe(
          map(x => this.dashboard$.next(x)),
          switchMap(x => this.dashboard$),
          map(x => this.form.patchValue({
            name: x.name
          }))
        )
        .subscribe();
  }

  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  public dashboard$: BehaviorSubject<Dashboard> = new BehaviorSubject(<Dashboard>{});
  
  public dashboardId: string;

  public handleCancelClick() {
    this._overlay.close();
  }

  public handleSaveClick() {
    const dashboard = new Dashboard();
    dashboard.dashboardId = this.dashboardId;
    dashboard.name = this.form.value.name;
    this._dashboardService.create({ dashboard })
      .pipe(
        map(x => dashboard.dashboardId = x.dashboardId),
        tap(x => this._overlay.close(dashboard)),
        takeUntil(this.onDestroy)
      )
      .subscribe();
  }

  public form: FormGroup = new FormGroup({
    name: new FormControl(null, [])
  });
} 

