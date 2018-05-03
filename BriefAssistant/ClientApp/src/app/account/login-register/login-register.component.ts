import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { finalize } from 'rxjs/operators/finalize';

import { LoginRequest } from "./LoginRequest";
import { RegistrationRequest } from "./RegistrationRequest";
import { AccountService } from '../../core/account.service';
import { UserType } from '../../shared/UserType';

@Component({
    selector: "loginRegister",
    templateUrl: "./login-register.component.html",
})
export class LoginRegisterComponent {
    loginModel = new LoginRequest();
    registerModel = new RegistrationRequest();
  public showRegisterSuccessDiv: boolean = false;
  public showRegisterFailDiv: boolean = false;
  public showLoginUnauthorizedDiv: boolean = false;
  public disableSignupButton: boolean = false;

    constructor(
      private readonly http: HttpClient,
      private readonly router: Router,
      private readonly accountService: AccountService
    ) {
      this.registerModel.userType = UserType.User;
    }

    login(form: NgForm) {
        this.showLoginUnauthorizedDiv = false;
      this.accountService.login(this.loginModel.email, this.loginModel.password).then(() => {
        var url = this.accountService.redirectUrl ? this.accountService.redirectUrl : '/home';
        this.accountService.redirectUrl = null;

        this.router.navigateByUrl(url);
      }).catch(() => {
        this.showLoginUnauthorizedDiv = true;
      });
    }

    onRegisterSubmit(form: NgForm) {
      this.showRegisterSuccessDiv = false;
      this.showRegisterFailDiv = false;
      this.disableSignupButton = true;
      var body = JSON.stringify(this.registerModel);
      let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

      this.http.post("/api/account/register", body, { headers: headers })
        .pipe(
        finalize(() => this.disableSignupButton = false)
        ).subscribe(res => {
          this.showRegisterSuccessDiv = true;
        }, err => {
          this.showRegisterFailDiv = true;
        });
    }
}
