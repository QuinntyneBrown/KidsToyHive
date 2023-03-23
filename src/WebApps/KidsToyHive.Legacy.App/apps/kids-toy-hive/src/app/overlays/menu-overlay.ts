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
