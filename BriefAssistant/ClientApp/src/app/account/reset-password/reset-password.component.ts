import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";

import { AccountService } from '../shared/account.service';
import { ResetPasswordRequest } from "../shared/ResetPasswordRequest";

@Component({
  selector: "resetPassword",
  templateUrl: "./reset-password.component.html",
})
export class ResetPasswordComponent implements OnInit {
  model = new ResetPasswordRequest();
  resetSuccessful: boolean;
  resetFailed: boolean;

  constructor(
    private readonly accountService: AccountService,
    private route: ActivatedRoute
  ) {
  }

  onSubmit(form: NgForm) {
    this.resetFailed = false;
    this.resetSuccessful = false;

    this.accountService.resetPassword(this.model)
      .subscribe(
        result => {
          this.resetSuccessful = true;
          this.resetFailed = false;
        },
        err => {
          this.resetSuccessful = false;
          this.resetFailed = true;
        }
      );
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.model.code = params['code'];
      this.model.email = params['email'];
    });
  }
}
