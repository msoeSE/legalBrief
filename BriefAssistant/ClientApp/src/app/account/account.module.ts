import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AccountComponent } from './account.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { PasswordValidator } from './shared/password-validator.directive';


@NgModule({
  imports: [
    SharedModule,
    AccountRoutingModule,
    HttpClientModule
  ],
  declarations: [
    AccountComponent,
    ConfirmationComponent,
    ForgotPasswordComponent,
    LoginRegisterComponent,
    ResetPasswordComponent,
    PasswordValidator
  ]
})
export class AccountModule { }
