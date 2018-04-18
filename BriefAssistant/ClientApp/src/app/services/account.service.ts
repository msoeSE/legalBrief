import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { OAuthService, OAuthInfoEvent } from 'angular-oauth2-oidc';
import { LoginRequest } from '../models/LoginRequest';

@Injectable()
export class AccountService {
  private autoRefreshSubscription: Subscription;

  constructor(private readonly oAuthService: OAuthService, private readonly router: Router) {}

  public get isLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  public get userName(): string {
    let claims = this.oAuthService.getIdentityClaims();
    if (!claims) return null;
    return claims['name'];
  }

  public login(loginModel: LoginRequest): Promise<any> {
    return this.oAuthService.fetchTokenUsingPasswordFlow(loginModel.email, loginModel.password)
      .then(() => {
        this.oAuthService.oidc = false;
          this.oAuthService.loadUserProfile().then(() => this.oAuthService.oidc = true);
        this.oAuthService.setupAutomaticSilentRefresh();
      });
  }

  private setupAutomaticRefresh() {
    if (!this.oAuthService.getRefreshToken()) {
      throw Error("Attempted to setup automatic refresh without having a refresh token");
    }

    this.autoRefreshSubscription = this.oAuthService.events.filter(e => e instanceof OAuthInfoEvent)
      .filter((e: OAuthInfoEvent) => e.type === 'token_expires' && e.info === 'access_token')
      .subscribe(e => {
        this.oAuthService.refreshToken();
      });
  }

  public logout() {
    if (this.autoRefreshSubscription) {
      this.autoRefreshSubscription.unsubscribe();
    }

    this.oAuthService.logOut();
    this.router.navigate(['']);
  }
}
