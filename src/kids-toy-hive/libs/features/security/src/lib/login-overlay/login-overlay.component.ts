import { Subject } from 'rxjs';
import { AfterContentInit, Component, ElementRef, EventEmitter, Input, Output, Renderer, OnDestroy } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthService } from '@kids-toy-hive/domain';
import { takeUntil, map } from 'rxjs/operators';
import { LocalStorageService, OverlayRefWrapper, accessTokenKey } from '@kids-toy-hive/core';

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
    private readonly _localStorageService: LocalStorageService,
    private readonly _overlayRefWrapper: OverlayRefWrapper
  ) {

  }

  public tryToLogin() {
    this._authService.tryToLogin({ 
      username: this.form.value.username,
      password: this.form.value.password
    })
    .pipe(takeUntil(this.onDestroy),map(x => { 
      this._localStorageService.put({ name: accessTokenKey, value: x.accessToken });
      this._overlayRefWrapper.close();
    }))
    .subscribe();
  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
