import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { EmailRequest } from "../../models/EmailRequest";
import { finalize } from 'rxjs/operators/finalize';

@Component({
    selector: "forgotPassword",
    templateUrl: "./forgotPassword.component.html",
})
export class ForgotPasswordComponent {
  model = new EmailRequest();
  public showEmailSentNotification: boolean = false;
  public disableButton: boolean = false;

    constructor(
        private readonly http: HttpClient
    ) { }

    onSubmit(form: NgForm) {
      this.disableButton = true;
      var body = JSON.stringify(this.model);
      let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      this.showEmailSentNotification = false;
      this.http.post("/api/account/forgotPassword", body, { headers: headers })
        .pipe(
        finalize(() => this.disableButton = false)
        ).subscribe(res => {
          this.showEmailSentNotification = true;
        });
    }
}
