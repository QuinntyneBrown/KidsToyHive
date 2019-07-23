import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  selector: 'kth-login'
})
export class LoginComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();
  username:string;
  password:string;

  public form = new FormGroup({
    username: new FormControl(this.username, [Validators.required]),
    password: new FormControl(this.password, [Validators.required])
  });
  
  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
