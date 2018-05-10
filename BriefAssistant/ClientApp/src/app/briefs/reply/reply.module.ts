import { NgModule } from '@angular/core';

import { ReplyRoutingModule } from './reply-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { ReplyComponent } from './reply.component';
import { ReplyExampleComponent } from './reply-example/reply-example.component';
import { ReplyFormComponent } from './reply-form/reply-form.component';
import { ReplyFinalComponent } from './reply-final/reply-final.component';
import { PendingChangesGuard } from '../../core/warning/warning-guard';

@NgModule({
  imports: [
    SharedModule,
    ReplyRoutingModule
  ],
  declarations: [
    ReplyComponent,
    ReplyExampleComponent,
    ReplyFormComponent,
    ReplyFinalComponent
  ],
  providers: [
    PendingChangesGuard
  ]
})
export class ReplyModule { }
