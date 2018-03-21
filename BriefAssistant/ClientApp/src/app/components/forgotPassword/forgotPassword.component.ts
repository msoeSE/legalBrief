import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { EmailRequest } from "../../models/EmailRequest"

@Component({
    selector: "forgotPassword",
    templateUrl: "./forgotPassword.component.html",
})
export class ForgotPasswordComponent {
model = new EmailRequest();

    constructor(
        private readonly http: HttpClient
    ) { }

    onSubmit(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        this.http.post("/api/account/forgotPassword", body, { headers: headers })
            .subscribe(res => {
                console.log(res);
            });
    }
}
