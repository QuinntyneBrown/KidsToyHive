import { Component, OnDestroy, Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { CustomerService, Customer, BookingService } from '@kids-toy-hive/domain';
import { LocalStorageService, accessTokenKey } from '@kids-toy-hive/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { takeUntil, map } from 'rxjs/operators';

@Injectable()
export class DoneSectionGuard implements CanActivate {
  constructor(
    private _localStorageService: LocalStorageService,
    private _router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    
    return true;
  }
}

@Component({
  templateUrl: './done-section.component.html',
  styleUrls: ['./done-section.component.css'],
  selector: 'kth-done-section'
})
export class DoneSectionComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
