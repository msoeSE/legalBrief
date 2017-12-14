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
        
    }
}