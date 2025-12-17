// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, Inject, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { baseUrl } from '../../core/constants';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { LoginDialog } from '../login-dialog/login-dialog';
import { Router } from '@angular/router';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { Header } from '../../components/header/header';
import { Menu } from '../menu/menu';

@Component({
  standalone: true,
  imports: [Header, Menu],
  templateUrl: './menu-dialog.html',
  styleUrls: ['./menu-dialog.scss'],
  selector: 'kth-menu-dialog'
})
export class MenuDialog implements OnInit { 
  constructor(
    @Inject(baseUrl)public apiBaseUrl:string, 
    private readonly _dialogRef: MatDialogRef<MenuDialog>,
    private readonly _dialog: MatDialog,
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

  public handleMenuClick() { this._dialogRef.close(); }

  public get imageUrl() {    
    return `${this.apiBaseUrl}api/digitalassets/serve/file/Logo.png`;
  }

  public signOut() {
    this._authService.logOut();
    this._dialogRef.close();
  }

  public signIn() {
    this._dialog.open(LoginDialog, {
      width: '400px',
      panelClass: 'login-dialog'
    });
    this._dialogRef.close();
  }

  public navigateByUrl(url) {
    this._router.navigateByUrl(url);
    this._dialogRef.close();
  }
}

