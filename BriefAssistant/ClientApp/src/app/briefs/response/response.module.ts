import { NgModule } from '@angular/core';

import { ResponseRoutingModule } from './response-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { ResponseComponent } from './response.component';
import { ResponseExampleComponent } from './response-example/response-example.component';
import { ResponseFormComponent } from './response-form/response-form.component';
import { ResponseFinalComponent } from './response-final/response-final.component';
import { PendingChangesGuard } from '../../core/warning/warning-guard';

@NgModule({
  imports: [
    SharedModule,
    ResponseRoutingModule
  ],
  declarations: [
    ResponseComponent,
    ResponseExampleComponent,
    ResponseFormComponent,
    ResponseFinalComponent
  ],
  providers: [
    PendingChangesGuard
  ]
})
export class ResponseModule { }
