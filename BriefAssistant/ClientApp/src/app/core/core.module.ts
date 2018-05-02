import { NgModule, Optional, SkipSelf } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { HeaderComponent } from './header/header.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';
import { AccountService } from './account.service';
import { AuthGuard } from './auth-guard';
import { throwIfAlreadyLoaded } from './module-import-guard';

@NgModule({
  imports: [SharedModule],
  declarations: [
    HeaderComponent,
    NotFoundComponent,
    PrivacyPolicyComponent
  ],
  providers: [
    AccountService,
    AuthGuard
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
