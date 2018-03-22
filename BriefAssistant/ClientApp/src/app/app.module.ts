import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { InitialFormComponent } from './components/forms/initial/initial-form.component';
import { HeaderComponent } from './components/header/header.component';
import { InitialFinalComponent } from './components/finals/initial/initial-final.component';
import { MainWelcomeComponent } from './components/welcomes/main/main-welcome.component';
import { InitialWelcomeComponent } from './components/welcomes/initial/initial-welcome.component';
import { ReplyWelcomeComponent } from './components/welcomes/reply/reply-welcome.component';
import { ResponseWelcomeComponent } from './components/welcomes/response/response-welcome.component';
import { PetitionWelcomeComponent } from './components/welcomes/petition/petition-welcome.component';
import { InitialExampleComponent } from './components/examples/initial/initial-example.component';
import { LoginRegisterComponent } from './components/loginRegister/loginRegister.component';
import { AccountPageComponent } from './components/accountPage/accountPage.component';
import { ForgotPasswordComponent } from './components/forgotPassword/forgotPassword.component';
import { ResetPasswordComponent } from './components/resetPassword/resetPassword.component';
import { ConfirmationComponent } from './components/confirmation/confirmation.component';
import { NotFoundComponent } from './components/notFound/notFound.component';
import { PasswordValidator } from "./passwordValidator.directive";
import { AuthGuard } from './auth.guard'
import { AccountService } from './services/account.service';
import { BriefService } from './services/brief.service';
import { OAuthModule } from 'angular-oauth2-oidc';

@
NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    InitialFormComponent,
    InitialFinalComponent,
    MainWelcomeComponent,
    InitialWelcomeComponent,
    ReplyWelcomeComponent,
    ResponseWelcomeComponent,
    PetitionWelcomeComponent,
    InitialExampleComponent,
    LoginRegisterComponent,
    AccountPageComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ConfirmationComponent,
    NotFoundComponent,
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
      { path: 'welcome', component: MainWelcomeComponent },
      { path: '', redirectTo: '/welcome', pathMatch: 'full' },
      { path: 'initial-welcome', component: InitialWelcomeComponent },
      { path: 'reply-welcome', component: ReplyWelcomeComponent },
      { path: 'response-welcome', component: ResponseWelcomeComponent },
      { path: 'petition-welcome', component: PetitionWelcomeComponent },
      { path: 'initial-final/:id', component: InitialFinalComponent, canActivate: [AuthGuard] },
      { path: 'initial-form', component: InitialFormComponent, canActivate: [AuthGuard] },
      { path: 'initial-example', component: InitialExampleComponent },
      { path: 'loginRegister', component: LoginRegisterComponent },
      { path: 'accountPage', component: AccountPageComponent, canActivate: [AuthGuard] },
      { path: 'forgotPassword', component: ForgotPasswordComponent },
      { path: 'resetPassword', component: ResetPasswordComponent},
      { path: 'confirmation', component: ConfirmationComponent },
      { path: '**', component: NotFoundComponent },
    ])
  ],
  exports: [RouterModule],
  providers: [
    BriefService,
    AuthGuard,
    AccountService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
