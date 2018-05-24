import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { OAuthModule } from 'angular-oauth2-oidc';
import { NgbCollapseModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module'
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CoreModule,
    SharedModule,
    HttpClientModule,
    NgbCollapseModule.forRoot(),
    NgbModalModule.forRoot(),
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: ['/api/'],
        sendAccessToken: true
      }
    }),
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    HomeComponent
  ],
  exports: [HttpClientModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
