// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

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

