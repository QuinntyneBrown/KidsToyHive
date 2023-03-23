import { Component, OnDestroy, OnInit, Inject } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { BookingService, Booking, AuthService } from '@kids-toy-hive/domain';
import { takeUntil, map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { baseUrl } from '@kids-toy-hive/core';

@Component({
  templateUrl: './my-profile-page.component.html',
  styleUrls: ['./my-profile-page.component.css'],
  selector: 'kth-my-profile-page'
})
export class MyProfilePageComponent implements OnInit, OnDestroy  { 
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
