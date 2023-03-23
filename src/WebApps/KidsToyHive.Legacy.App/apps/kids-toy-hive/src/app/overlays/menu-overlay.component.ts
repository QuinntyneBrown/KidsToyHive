// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, Inject, OnInit } from '@angular/core';
import { AuthService } from '@kids-toy-hive/domain';
import { baseUrl, OverlayRefWrapper, LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';
import { LoginOverlay } from '@kids-toy-hive/features/security';
import { Router } from '@angular/router';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';

@Component({
  templateUrl: './menu-overlay.component.html',
  styleUrls: ['./menu-overlay.component.css'],
  selector: 'kth-menu-overlay'
})
export class MenuOverlayComponent implements OnInit { 
  constructor(
    @Inject(baseUrl)public apiBaseUrl:string, 
    private readonly _overlayRef: OverlayRefWrapper,
    private readonly _loginOverlay: LoginOverlay,
    private readonly _router: Router,
    private readonly _authService: AuthService,
    private readonly _breakpointObserver: BreakpointObserver
  ) { }

  public get isAuthenticated():boolean {    
    return this._authService.isAuthenticated;
  }

  public isMobile = true;

  public ngOnInit() {
    this._breakpointObserver
    .observe(['(min-width: 768px)'])
    .subscribe((state: BreakpointState) => {
      this.isMobile = !state.matches;
    });
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

