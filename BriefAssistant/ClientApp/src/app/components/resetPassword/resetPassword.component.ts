import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { ResetPasswordRequest } from "../../models/ResetPasswordRequest";

@Component({
    selector: "resetPassword",
    templateUrl: "./resetPassword.component.html",
})
export class ResetPasswordComponent implements OnInit{
    private model = new ResetPasswordRequest();

    constructor(
      private readonly http: HttpClient,
      private route: ActivatedRoute
    ) {}

    onSubmit(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        this.http.post("/api/account/resetPassword", body, { headers: headers })
            .subscribe(res => {
                console.log(res);
            });
    }

    ngOnInit() {
      this.route.queryParams.subscribe(params => {
        this.model.code = params['code'];
      });
    }
}
