import { Component, Inject } from '@angular/core';
import { baseUrl } from '@kids-toy-hive/core';
import { LoginOverlay } from '@kids-toy-hive/features/security';
import { MenuOverlay } from './overlays';

@Component({
  selector: 'kth-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(
    @Inject(baseUrl)public apiBaseUrl:string,
    private readonly _menuOverlay: MenuOverlay
  ) { 

  }

  public handleMenuClick() {
    this._menuOverlay.create();
  }
  public get imageUrl() {
    return `${this.apiBaseUrl}api/digitalassets/serve/file/Logo.png`;
  }
}