// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { baseUrl, LocalStorageService } from './core';
import { MatDialog } from '@angular/material/dialog';
import { MenuDialog } from './dialogs/menu-dialog/menu-dialog';
import { Header } from './components/header/header';
import { Footer } from './components/footer/footer';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, Header, Footer],
  selector: 'kth-root',
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App implements OnInit {
  constructor(
    @Inject(baseUrl)public apiBaseUrl:string,
    private readonly _dialog: MatDialog,
    private readonly _localStorageService: LocalStorageService,
    private readonly _router: Router
  ) { 

  }

  ngOnInit() {

  }

  public handleMenuClick() {
    this._dialog.open(MenuDialog, {
      width: '100vw',
      height: '100vh',
      maxWidth: '100vw',
      panelClass: 'full-screen-dialog'
    });
  }

  public handleLogoClick() {
    this._router.navigateByUrl('/');
  }

  public get imageUrl() {
    return `${this.apiBaseUrl}api/digitalassets/serve/file/Logo.png`;
  }
}
