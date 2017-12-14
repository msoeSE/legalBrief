import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { Http, Headers } from "@angular/http";
import { ResetPasswordRequest } from "../../models/ResetPasswordRequest";

@Component({
    selector: "resetPassword",
    templateUrl: "./resetPassword.component.html",
})
export class ResetPasswordComponent {
    private model = new ResetPasswordRequest();

    constructor(
        private readonly http: Http
    ) {}

    onSubmit(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new Headers({ 'Content-Type': 'application/json' });

        this.http.post("/api/account/resetPassword", body, { headers: headers })
            .map(res => res.json())
            .subscribe(data => {
                console.log(data);
            });
    }
}