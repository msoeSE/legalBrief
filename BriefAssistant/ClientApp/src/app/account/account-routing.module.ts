import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AccountComponent } from './account.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component'
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const accountRoutes: Routes = [
  {
    path: '',
    component: AccountComponent,
    children: [
      { path: 'confirmation', component: ConfirmationComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent},
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
