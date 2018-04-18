import {
  Component,
  ChangeDetectionStrategy,
  Input,
  OnInit,
  EventEmitter,
  Output,
  AfterViewInit,
  AfterContentInit,
  Renderer,
  ElementRef,
  ViewEncapsulation,
  HostListener
} from "@angular/core";

import {
  FormGroup,
  FormControl,
  Validators
} from "@angular/forms";

import { constants } from "../shared/constants";


@Component({
  selector: 'app-login',
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent {  
  @Input()
  public username: string;

  @Input()
  public password: string;

  @Input()
  public rememberMe: boolean;

  @Input()
  public errorMessage: string = "";
  
  public form = new FormGroup({
    username: new FormControl(this.username, [Validators.required]),
    password: new FormControl(this.password, [Validators.required]),
    rememberMe: new FormControl(this.rememberMe, [])
  });
  
  @Output()
  public tryToLogin: EventEmitter<any> = new EventEmitter();  
}
