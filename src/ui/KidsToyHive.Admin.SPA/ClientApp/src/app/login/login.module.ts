import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { LoginPageComponent } from './login-page.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    LoginComponent,
    LoginPageComponent
  ],
  exports: [
    LoginComponent,
    LoginPageComponent
  ]
})
export class LoginModule { }
