import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { Http, Headers } from "@angular/http";
import { EmailRequest } from "../../models/EmailRequest"

@Component({
    selector: "forgotPassword",
    templateUrl: "./forgotPassword.component.html",
})
export class ForgotPasswordComponent {
    private model = new EmailRequest();

    constructor(
        private readonly http: Http
    ) { }

    onSubmit(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new Headers({ 'Content-Type': 'application/json' });

        this.http.post("/api/account/forgotPassword", body, { headers: headers })
            .map(res => res.json())
            .subscribe(data => {
                console.log(data);
            });
    }
}