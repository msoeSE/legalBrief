import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module'
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

import { OAuthModule } from 'angular-oauth2-oidc';

@
NgModule({
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CoreModule,
    SharedModule,
    HttpClientModule,
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: ["/api/"],
        sendAccessToken: true
      }
    }),
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    HomeComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
