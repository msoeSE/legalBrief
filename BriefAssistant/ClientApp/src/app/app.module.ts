import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { FormComponent } from './components/form/form.component';
import { FinalComponent } from './components/final/final.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { ExampleComponent } from './components/example/example.component';
import { LoginRegisterComponent } from './components/loginRegister/loginRegister.component';
import { AccountPageComponent } from './components/accountPage/accountPage.component';
import { ForgotPasswordComponent } from './components/forgotPassword/forgotPassword.component';
import { ResetPasswordComponent } from './components/resetPassword/resetPassword.component';
import { PasswordValidator } from "./passwordValidator.directive";
import {AuthGuard} from './auth.guard'
import { BriefService } from './services/brief.service';
import { OAuthModule } from 'angular-oauth2-oidc';

@
NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FormComponent,
    FinalComponent,
    WelcomeComponent,
    ExampleComponent,
    LoginRegisterComponent,
    AccountPageComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    PasswordValidator
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: ["/api/"],
        sendAccessToken: true
      }
    }),
    RouterModule.forRoot([
      { path: '', component: WelcomeComponent, pathMatch: 'full' },
      { path: 'final/:id', component: FinalComponent, canActivate: [AuthGuard] },
      { path: 'dataform', component: FormComponent, canActivate: [AuthGuard] },
      { path: 'example', component: ExampleComponent },
      { path: 'loginRegister', component: LoginRegisterComponent },
      { path: 'accountPage', component: AccountPageComponent, canActivate: [AuthGuard] },
      { path: 'forgotPassword', component: ForgotPasswordComponent },
      { path: 'resetPassword', component: ResetPasswordComponent, canActivate: [AuthGuard] }
    ])
  ],
  exports: [RouterModule],
  providers: [BriefService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
