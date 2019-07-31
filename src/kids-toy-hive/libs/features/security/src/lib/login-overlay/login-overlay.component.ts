import { Subject } from 'rxjs';
import { AfterContentInit, Component, ElementRef, EventEmitter, Input, Output, Renderer, OnDestroy } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  templateUrl: './login-overlay.component.html',
  styleUrls: ['./login-overlay.component.css'],
  selector: 'kth-login-overlay'
})
export class LoginOverlayComponent implements OnDestroy  { 
  public onDestroy: Subject<void> = new Subject<void>();

  @Output()
  public tryToLogin:EventEmitter<any> = new EventEmitter();
  
  public form = new FormGroup({
    username: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required])    
  });

  constructor() {

  }

  ngOnDestroy() {
    this.onDestroy.next();	
  }
}
