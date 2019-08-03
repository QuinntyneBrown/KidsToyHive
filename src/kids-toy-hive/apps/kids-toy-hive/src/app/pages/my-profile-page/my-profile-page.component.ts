import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { BookingService, Booking, AuthService } from '@kids-toy-hive/domain';
import { takeUntil, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  templateUrl: './my-profile-page.component.html',
  styleUrls: ['./my-profile-page.component.css'],
  selector: 'kth-my-profile-page'
})
export class MyProfilePageComponent implements OnInit, OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  public bookings$:Observable<Booking[]>;

  constructor(
    private readonly _authService: AuthService,
    private readonly _bookingService: BookingService,
    private readonly _router: Router
  ) { }
  
  public ngOnInit() {
    this.bookings$ = this._bookingService.getMy();

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
