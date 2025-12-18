// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Subject } from 'rxjs';
import { Component, OnDestroy } from "@angular/core";
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from "@angular/forms";
import { AuthService } from '../../core/services/auth.service';
import { takeUntil, map } from 'rxjs/operators';
import { MatDialogRef } from '@angular/material/dialog';
import { ProblemDetails, isProblemDetails } from '../../core/problem-details';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { Button } from '../../components/button/button';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatIconModule, Button],
  templateUrl: './login-dialog.html',
  styleUrls: ['./login-dialog.scss'],
  selector: 'kth-login-dialog'
})
export class LoginDialog implements OnDestroy  {
  public onDestroy: Subject<void> = new Subject<void>();

  public form = new FormGroup({
    username: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required])    
  });

  constructor(
    private readonly _authService: AuthService,
    private readonly _dialogRef: MatDialogRef<LoginDialog>
  ) { }

  public errorMessage:string;
  
  public close() {
    this._dialogRef.close();
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
            this._dialogRef.close();
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

