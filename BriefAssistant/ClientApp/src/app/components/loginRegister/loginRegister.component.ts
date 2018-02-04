import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OAuthService } from 'angular-oauth2-oidc';

import { LoginRequest } from "../../models/LoginRequest";
import { RegistrationRequest } from "../../models/RegistrationRequest";

@Component({
    selector: "loginRegister",
    templateUrl: "./loginRegister.component.html",
})
export class LoginRegisterComponent {
    private loginModel = new LoginRequest();
    private registerModel = new RegistrationRequest();
    public showRegisterSuccessDiv: boolean = false;
    public showLoginUnauthorizedDiv: boolean = false;

    constructor(
      private oAuthService: OAuthService,
		private readonly http: HttpClient,
        private readonly router: Router
    ) { }

    login(form: NgForm) {
        this.showLoginUnauthorizedDiv = false;
        this.oAuthService.fetchTokenUsingPasswordFlow(this.loginModel.email, this.loginModel.password)
        .then(() => {
          this.oAuthService.setupAutomaticSilentRefresh();
          this.router.navigate(['']);
        });
    }

    onRegisterSubmit(form: NgForm) {
        this.showRegisterSuccessDiv = false;
        var body = JSON.stringify(this.registerModel);
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        this.http.post("/api/account/register", body, { headers: headers })
			.subscribe(res => {
                console.log(res);
                this.showRegisterSuccessDiv = true;
	        });
    }
}
