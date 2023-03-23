// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Injector } from '@angular/core';
import { Logger, BaseOverlayService, OverlayRefProvider } from '@kids-toy-hive/core';
import { MenuOverlayComponent } from './menu-overlay.component';

@Injectable()
export class MenuOverlay extends BaseOverlayService<MenuOverlayComponent> {
  constructor(
    public injector: Injector,
    public overlayRefProvider: OverlayRefProvider,
    public logger: Logger
  ) {
    super(injector, overlayRefProvider, MenuOverlayComponent, logger);
  }
}

