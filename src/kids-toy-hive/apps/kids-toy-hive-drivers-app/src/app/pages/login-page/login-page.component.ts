import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
  selector: 'login-page-login-page'
})
export class LoginPageComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
