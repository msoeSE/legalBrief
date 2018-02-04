import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { FormComponent } from './components/form/form.component';
import { FinalComponent } from './components/final/final.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { ExampleComponent } from './components/example/example.component';
import { LoginRegisterComponent } from "./components/loginRegister/loginRegister.component";
import { AccountPageComponent } from "./components/accountPage/accountPage.component";
import { ForgotPasswordComponent } from "./components/forgotPassword/forgotPassword.component";
import { ResetPasswordComponent } from "./components/resetPassword/resetPassword.component";
import { PasswordValidator } from "./passwordValidator.directive";
import { BriefService } from './services/brief.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
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
    RouterModule.forRoot([
      { path: '', component: WelcomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'final/:id', component: FinalComponent },
      { path: 'dataform', component: FormComponent },
      { path: 'example', component: ExampleComponent },
      { path: 'loginRegister', component: LoginRegisterComponent },
      { path: 'accountPage', component: AccountPageComponent },
      { path: 'forgotPassword', component: ForgotPasswordComponent },
      { path: 'resetPassword', component: ResetPasswordComponent }
    ])
  ],
  exports: [RouterModule],
  providers: [BriefService],
  bootstrap: [AppComponent]
})
export class AppModule { }
