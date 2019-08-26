import { Component } from '@angular/core';
import { LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';
import { Router } from '@angular/router';

@Component({
  selector: 'kth-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router
  ) {

  }

  public tryToLogout() {
    this._localStorageService.remove({ name: accessTokenKey });
    this._router.navigateByUrl('/login');
  }
}
