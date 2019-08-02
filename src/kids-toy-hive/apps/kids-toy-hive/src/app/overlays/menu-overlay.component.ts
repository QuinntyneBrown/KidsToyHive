import { Component, Inject } from '@angular/core';
import { AuthService } from '@kids-toy-hive/domain';
import { baseUrl, OverlayRefWrapper, LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';
import { LoginOverlay } from '@kids-toy-hive/features/security';
import { Router } from '@angular/router';

@Component({
  templateUrl: './menu-overlay.component.html',
  styleUrls: ['./menu-overlay.component.css'],
  selector: 'kth-menu-overlay'
})
export class MenuOverlayComponent  { 
  constructor(
    @Inject(baseUrl)public apiBaseUrl:string, 
    private readonly _overlayRef: OverlayRefWrapper,
    private readonly _loginOverlay: LoginOverlay,
    private readonly _router: Router,
    private readonly _authService: AuthService,
  ) { }

  public get isAuthenticated():boolean {    
    return this._authService.isAuthenticated;
  }

  public handleMenuClick() { this._overlayRef.close(); }

  public get imageUrl() {    
    return `${this.apiBaseUrl}api/digitalassets/serve/file/Logo.png`;
  }

  public signOut() {
    this._authService.logOut();
    this._overlayRef.close();
  }

  public signIn() {
    this._loginOverlay.create();
    this._overlayRef.close();
  }

  public navigateByUrl(url) {
    this._router.navigateByUrl(url);
    this._overlayRef.close();
  }
}
