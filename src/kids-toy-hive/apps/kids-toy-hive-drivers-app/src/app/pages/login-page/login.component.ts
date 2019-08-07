import { Component, OnDestroy, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  selector: 'kth-login'
})
export class LoginComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  public username: string;
  public password: string;
  public errorMessage: string;
  public login:EventEmitter<any> = new EventEmitter();

  public form = new FormGroup({
    username: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required])
  });

  public tryToLogin(event$:any) {

  }


  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
