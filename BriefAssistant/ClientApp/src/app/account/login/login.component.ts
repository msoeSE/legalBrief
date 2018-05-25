import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { faUser, faLock } from '@fortawesome/free-solid-svg-icons';

import { LoginRequest } from './LoginRequest';
import { AuthService } from '../../core/auth.service';

@Component({
  templateUrl: "./login.component.html"
})
export class LoginComponent {
  // Expose icon to template
  faUser = faUser;
  faLock = faLock;

  loginModel = new LoginRequest();
  public showLoginUnauthorizedDiv: boolean = false;

  constructor(
    private readonly router: Router,
    private readonly authService: AuthService,
  ) {
   
  }

  login(form: NgForm) {
    this.showLoginUnauthorizedDiv = false;
    this.authService.login(this.loginModel.email, this.loginModel.password).then(() => {
      var url = this.authService.redirectUrl ? this.authService.redirectUrl : '/home';
      this.authService.redirectUrl = null;

      this.router.navigateByUrl(url);
    }).catch(() => {
      this.showLoginUnauthorizedDiv = true;
    });
  }
}
