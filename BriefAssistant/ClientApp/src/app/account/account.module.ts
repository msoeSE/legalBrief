import { NgModule } from '@angular/core';

import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AccountComponent } from './account.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { PasswordValidator } from './shared/password-validator.directive';

import { AccountService } from './shared/account.service';


@NgModule({
  imports: [
    SharedModule,
    AccountRoutingModule,
  ],
  declarations: [
    AccountComponent,
    ConfirmationComponent,
    ForgotPasswordComponent,
    LoginRegisterComponent,
    ResetPasswordComponent,
    PasswordValidator
  ],
  providers: [AccountService]
})
export class AccountModule { }
