import { Component, ViewChild } from '@angular/core';
import { LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { MatSidenav } from '@angular/material/sidenav';

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
    this._localStorageService.changes$
    .pipe(
      tap(x => {
          this.loggedIn = this._localStorageService.get({ name: accessTokenKey }) != null;
      })
    )
    .subscribe();
  }

  @ViewChild(MatSidenav, { static: false })
  public drawer:MatSidenav;

  public tryToLogout() {
    this._localStorageService.remove({ name: accessTokenKey });
    this._router.navigateByUrl('/login');
  }

  private _loggedIn:boolean;

  public get loggedIn():boolean {
    return this._loggedIn;
  }

  public set loggedIn(value:boolean) {

    if(value !== this._loggedIn && this.drawer) 
      this.drawer.close();

    this._loggedIn = value;
  }
}
