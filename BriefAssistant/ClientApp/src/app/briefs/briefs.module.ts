import { NgModule } from '@angular/core';

import { BriefsRoutingModule } from './briefs-routing.module';
import { SharedModule } from '../shared/shared.module';
import { BriefsComponent } from './briefs.component';
import { BriefListComponent } from './brief-list/brief-list.component';

import { BriefSelectionComponent } from './brief-selection/brief-selection.component';
import { BriefService } from './shared/brief.service';
import { PagerService } from './brief-list/pager.service';

@NgModule({
  imports: [
    SharedModule,
    BriefsRoutingModule
  ],
  declarations: [
    BriefsComponent,
    BriefListComponent,
    BriefSelectionComponent
  ],
  providers: [
    BriefService, PagerService
  ]
})
export class BriefsModule { }
