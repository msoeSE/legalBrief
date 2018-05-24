import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";

import { finalize } from 'rxjs/operators/finalize';
import { RegistrationRequest } from "../shared/RegistrationRequest";
import { UserType } from '../../shared/UserType';
import { AccountService } from '../shared/account.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {

  registerModel = new RegistrationRequest();
  showRegisterSuccessDiv: boolean = false;
  showRegisterFailDiv: boolean = false;
  disableSignupButton: boolean = false;

  constructor(private readonly accountService: AccountService) {
    this.registerModel.userType = UserType.User;
  }

  onRegisterSubmit(form: NgForm) {
    this.showRegisterSuccessDiv = false;
    this.showRegisterFailDiv = false;
    this.disableSignupButton = true;
    this.accountService.register(this.registerModel)
      .pipe(
        finalize(() => this.disableSignupButton = false)
      ).subscribe(
        res => this.showRegisterSuccessDiv = true,
        err => this.showRegisterFailDiv = true
      );
  }
}
