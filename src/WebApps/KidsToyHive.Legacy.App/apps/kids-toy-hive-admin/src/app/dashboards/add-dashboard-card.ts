import { Injectable, Injector } from '@angular/core';
//import { BaseOverlayService, OverlayRefProvider } from '@k/domain';
import {} from '@kids-toy-hive/core';
import { AddDashboardCardComponent } from './add-dashboard-card.component';

@Injectable()
export class AddDashboardCard extends BaseOverlayService<AddDashboardCardComponent> {
  constructor(
    public injector: Injector,
    public overlayRefProvider: OverlayRefProvider,
    public logger: Logger
  ) {
    super(injector, overlayRefProvider, AddDashboardCardComponent, logger);
  }
}
