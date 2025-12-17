// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ComponentType } from "@angular/cdk/overlay";
import { ComponentPortal } from "@angular/cdk/portal";
import { ComponentRef, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { OverlayRefProvider } from "./overlay-ref-provider";
import { Logger } from './logger';
import { OverlayRefWrapper } from './overlay-ref-wrapper';

export class BaseOverlayService<TComponent> {
  constructor(
    public _injector: Injector,
    public _overlayRefProvider: OverlayRefProvider,
    private _component: ComponentType<TComponent>,
    private _logger: Logger
  ) { }

  public create(options: { source?: any, injectionTokens?: Map<any, any> } = {}): Observable<any> {
    const overlayRef = this._overlayRefProvider.create();    
    const overlayRefWrapper = new OverlayRefWrapper(overlayRef,this._logger);
    options.injectionTokens = options.injectionTokens || new Map();
    options.injectionTokens.set(OverlayRefWrapper, overlayRefWrapper);
    const overlayComponent = this.attachOverlayContainer(overlayRef, options.injectionTokens);
    Object.assign(overlayComponent, options.source);
    return overlayRefWrapper.afterClosed();
  }

  private attachOverlayContainer(overlayRef, injectionTokens: Map<any, any>) {
    const injector = Injector.create({
      parent: this._injector,
      providers: Array.from(injectionTokens.entries()).map(([token, value]) => ({ provide: token, useValue: value }))
    });
    const overlayPortal = new ComponentPortal(this._component, null, injector);
    const overlayPortalRef: ComponentRef<TComponent> = overlayRef.attach(overlayPortal);
    return overlayPortalRef.instance;
  }
}
