import { Component, Inject, OnInit } from '@angular/core';
import { baseUrl, LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';
import { MenuOverlay } from './overlays';
import { Router } from '@angular/router';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';

@Component({
  selector: 'kth-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(
    @Inject(baseUrl)public apiBaseUrl:string,
    private readonly _menuOverlay: MenuOverlay,
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router,
    public breakpointObserver: BreakpointObserver
  ) { 

  }

  ngOnInit() {

  }

  public handleMenuClick() {
    this._menuOverlay.create();
  }

  public handleLogoClick() {
    this._router.navigateByUrl('/');
  }

  public get imageUrl() {
    return `${this.apiBaseUrl}api/digitalassets/serve/file/Logo.png`;
  }
}