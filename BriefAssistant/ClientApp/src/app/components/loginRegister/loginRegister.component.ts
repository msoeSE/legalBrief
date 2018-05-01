import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { LoginRequest } from "../../models/LoginRequest";
import { RegistrationRequest } from "../../models/RegistrationRequest";
import { AccountService } from '../../services/account.service';
import { UserType } from '../../models/UserType';

@Component({
    selector: "loginRegister",
    templateUrl: "./loginRegister.component.html",
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
      this.accountService.login(this.loginModel).then(() => {
        this.router.navigate(['']);
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
          .subscribe(res => {
                this.showRegisterSuccessDiv = true;
          }, err => {
            this.showRegisterFailDiv = true;
          });
        this.disableSignupButton = true;
    }
}
