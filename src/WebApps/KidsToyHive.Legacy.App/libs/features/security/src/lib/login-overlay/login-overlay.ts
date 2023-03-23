// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, Injector } from '@angular/core';
import { LoginOverlayComponent } from './login-overlay.component';
import { BaseOverlayService, OverlayRefProvider, Logger } from '@kids-toy-hive/core';

@Injectable()
export class LoginOverlay extends BaseOverlayService<LoginOverlayComponent> {
  constructor(
    public injector: Injector,
    public overlayRefProvider: OverlayRefProvider,
    public logger: Logger
  ) {
    super(injector, overlayRefProvider, LoginOverlayComponent, logger);
  }
}

