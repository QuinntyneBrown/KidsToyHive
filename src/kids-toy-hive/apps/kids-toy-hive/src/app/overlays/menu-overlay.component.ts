import { Component, OnDestroy, Inject } from '@angular/core';
import { Subject } from 'rxjs';
import { CustomerService, BookingService } from '@kids-toy-hive/domain';
import { baseUrl, OverlayRefWrapper } from '@kids-toy-hive/core';
import { LoginOverlay } from '@kids-toy-hive/features/security';

@Component({
  templateUrl: './menu-overlay.component.html',
  styleUrls: ['./menu-overlay.component.css'],
  selector: 'kth-menu-overlay'
})
export class MenuOverlayComponent  { 
  constructor(
    @Inject(baseUrl)public apiBaseUrl:string, 
    private readonly _overlayRef: OverlayRefWrapper,
    private readonly _bookingService: BookingService,
    private readonly _customerService: CustomerService,
    private readonly _loginOverlay: LoginOverlay 
  ) { 

  }

  public handleMenuClick() { this._overlayRef.close(); }

  public get imageUrl() {
    return `${this.apiBaseUrl}api/digitalassets/serve/file/Logo.png`;
  }

  public signIn() {
    this._loginOverlay.create();
    this._overlayRef.close();
  }
}
