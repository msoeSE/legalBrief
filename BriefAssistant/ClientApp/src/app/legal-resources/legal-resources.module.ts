import { NgModule } from '@angular/core';

import { LegalResourcesRoutingModule } from './legal-resources-routing.module';
import { SharedModule } from '../shared/shared.module';
import { LegalResourcesComponent } from './legal-resources.component';

@NgModule({
  imports: [
    LegalResourcesRoutingModule,
    SharedModule,
  ],
  declarations: [
    LegalResourcesComponent
  ]
})
export class LegalResourcesModule { }
