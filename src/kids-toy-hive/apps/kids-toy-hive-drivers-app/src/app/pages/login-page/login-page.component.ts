import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { AuthService } from '@kids-toy-hive/domain';

@Component({
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
  selector: 'kth-login-page'
})
export class LoginPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  constructor(private readonly _authService: AuthService) {

  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
