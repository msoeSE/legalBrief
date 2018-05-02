import { Component, Inject } from '@angular/core';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { AccountService } from './core/account.service';

import { authConfig } from './auth.config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onActivate(event) {
    window.scroll(0, 0);
  }

  constructor(private oAuthService: OAuthService, @Inject('BASE_URL') private baseUrl: string, public accountService: AccountService) {
    this.oAuthService.configure(authConfig(this.baseUrl));
    //this.oAuthService.setStorage(localStorage);
    this.oAuthService.tokenValidationHandler = new JwksValidationHandler();
    this.oAuthService.loadDiscoveryDocumentAndTryLogin();
  }
}
