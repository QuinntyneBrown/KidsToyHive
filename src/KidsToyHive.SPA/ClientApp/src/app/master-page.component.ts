import { Component, HostBinding, ElementRef } from '@angular/core';
import { AuthService } from './core/auth.service';
import { map, tap, takeUntil } from 'rxjs/operators';
import { LoginRedirectService } from './core/redirect.service';
import { AppService } from './core/app.service';

@Component({
  templateUrl: './master-page.component.html',
  styleUrls: ['./master-page.component.css'],
  selector: 'app-master-page'
})
export class MasterPageComponent {
  constructor(
    private _authService: AuthService,
    private _appService: AppService,
    private _loginRedirect: LoginRedirectService
  ) { }
  
  ngDoCheck() {
    this._appService.checks$.next(null);
  }
 
  public signOut() {
    this._authService.logout();
    this._loginRedirect.redirectToLogin();
  }

  public closeErrorConsole() {
    this.isErrorConsoleOpen = false;
  }

  public clearNotifcations() {

  }

  @HostBinding("class.error-console-is-opened")
  public isErrorConsoleOpen:boolean = null;
}
