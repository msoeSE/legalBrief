import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";

import { AccountService } from '../shared/account.service';
import { EmailRequest } from "../../shared/EmailRequest";
import { finalize } from 'rxjs/operators/finalize';

@Component({
  selector: "forgotPassword",
  templateUrl: "./forgot-password.component.html",
})
export class ForgotPasswordComponent {
  model = new EmailRequest();
  public showEmailSentNotification: boolean = false;
  public disableButton: boolean = false;

  constructor(
    private readonly accountService: AccountService
  ) {
  }

  onSubmit(form: NgForm) {
    this.disableButton = true;
    this.accountService.forgotPassword(this.model)
      .pipe(
        finalize(() => this.disableButton = false)
      ).subscribe(res => {
        this.showEmailSentNotification = true;
      });
  }
}
