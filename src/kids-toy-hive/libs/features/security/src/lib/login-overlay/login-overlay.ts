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
