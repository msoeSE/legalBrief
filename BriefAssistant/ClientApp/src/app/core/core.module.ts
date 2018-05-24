import { NgModule, Optional, SkipSelf } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

import { SharedModule } from '../shared/shared.module';
import { HeaderComponent } from './header/header.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth-guard';
import { throwIfAlreadyLoaded } from './module-import-guard';
import { PendingChangesGuard } from './warning-guard';

@NgModule({
  imports: [
    SharedModule,
    RouterModule,
    NgbCollapseModule,
    FontAwesomeModule
  ],
  declarations: [
    HeaderComponent,
    NotFoundComponent,
    PrivacyPolicyComponent
  ],
  providers: [
    AuthService,
    AuthGuard,
    PendingChangesGuard
  ],
  exports: [
    HeaderComponent
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
