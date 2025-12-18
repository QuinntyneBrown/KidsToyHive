// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, OnDestroy, OnInit, Inject } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { BookingService, Booking, AuthService } from '../../core';
import { takeUntil, map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { baseUrl } from '../../core';

@Component({
  standalone: true,
  templateUrl: './my-profile-page.html',
  styleUrls: ['./my-profile-page.scss'],
  selector: 'kth-my-profile-page'
})
export class MyProfilePage implements OnInit, OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public bookings$:Observable<Booking[]>;

  constructor(
    @Inject(baseUrl) private readonly _baseUrl:string,
    private readonly _authService: AuthService,
    private readonly _bookingService: BookingService,
    private readonly _router: Router
  ) { }
  
  public ngOnInit() {
    this.bookings$ = this._bookingService.getMy()
    .pipe(map(x => {
      return x as Booking[];
    }));

    this._authService.isAuthenticatedChanged$
    .pipe(takeUntil(this.onDestroy),map(x => {            
      if(!x) { 
        this._router.navigateByUrl('toys'); 
      }
    }))
    .subscribe();
  }

  public ngOnDestroy() { this.onDestroy.next(); }
}

