import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

import { AccountService } from './account.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private readonly oauthService: OAuthService,
    private readonly router: Router,
    private readonly accountService: AccountService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.oauthService.hasValidAccessToken()) {
      return true;
    }

    this.accountService.redirectUrl = state.url;
    this.router.navigateByUrl('/account/login-register');
    return false;
  }
}
