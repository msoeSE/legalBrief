import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { ResetPasswordRequest } from "../../models/ResetPasswordRequest";

@Component({
    selector: "resetPassword",
    templateUrl: "./resetPassword.component.html",
})
export class ResetPasswordComponent {
    private model = new ResetPasswordRequest();

    constructor(
        private readonly http: HttpClient
    ) {}

    onSubmit(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        this.http.post("/api/account/resetPassword", body, { headers: headers })
            .subscribe(res => {
                console.log(res);
            });
    }
}
