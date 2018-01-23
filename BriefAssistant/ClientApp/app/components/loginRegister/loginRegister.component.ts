import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from "@angular/forms";
import { Http, Headers } from '@angular/http';
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
		private readonly http: Http,
        private readonly router: Router
    ) { }

    onLoginSubmit(form: NgForm) {
        this.showLoginUnauthorizedDiv = false;
        var body = JSON.stringify(this.loginModel);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.post("/api/account/login", body, { headers: headers })
            .map(res => res.json())
            .subscribe(data => {
                console.log(data);
                this.router.navigate(["/example"]);
            }, data1 => {
                if (data1.status === 401) {
                    this.showLoginUnauthorizedDiv = true;
                }
            });
    }

    onRegisterSubmit(form: NgForm) {
        this.showRegisterSuccessDiv = false;
        var body = JSON.stringify(this.registerModel);
        let headers = new Headers({ 'Content-Type': 'application/json' });

        this.http.post("/api/account/register", body, { headers: headers })
            .map(res => res.json())
			.subscribe(data => {
                console.log(data);
                this.showRegisterSuccessDiv = true;
	        });
    }
}