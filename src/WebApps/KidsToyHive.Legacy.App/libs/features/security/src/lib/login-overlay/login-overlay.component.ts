import { Subject } from 'rxjs';
import { Component, OnDestroy } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthService } from '@kids-toy-hive/domain';
import { takeUntil, map } from 'rxjs/operators';
import { OverlayRefWrapper, ProblemDetails, isProblemDetails } from '@kids-toy-hive/core';

@Component({
  templateUrl: './login-overlay.component.html',
  styleUrls: ['./login-overlay.component.css'],
  selector: 'kth-login-overlay'
})
export class LoginOverlayComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  public form = new FormGroup({
    username: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required])    
  });

  constructor(
    private readonly _authService: AuthService,
    private readonly _overlayRefWrapper: OverlayRefWrapper
  ) { }

  public errorMessage:string;
  
  public close() {
    this._overlayRefWrapper.close();
  }
  
  public tryToLogin() {
    if(this.form.valid) {
      this._authService.tryToLogin({ 
        username: this.form.value.username,
        password: this.form.value.password
      })
      .pipe(
        takeUntil(this.onDestroy),
        map((x: ProblemDetails) => {           
          if(isProblemDetails(x)) {
            this.errorMessage = 'Invalid Username or Password.';
          } 
          else {
            this._overlayRefWrapper.close();
          }          
        }))
        .subscribe();
    } 
    else {
      this.errorMessage = 'Missing required fields.';      
    }
  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }

  handleInputFocus() { this.errorMessage = ''; }
}
