import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AccountComponent } from './account.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const accountRoutes: Routes = [
  {
    path: '',
    component: AccountComponent,
    children: [
      { path: 'confirmation', component: ConfirmationComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'login-register', component: LoginRegisterComponent },
      { path: 'reset-password', component: ResetPasswordComponent }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(accountRoutes)
  ],
  exports: [RouterModule]
})
export class AccountRoutingModule {
}
