import { NgModule } from '@angular/core';

import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { BriefsRoutingModule } from './briefs-routing.module';
import { SharedModule } from '../shared/shared.module';
import { BriefsComponent } from './briefs.component';
import { BriefListComponent } from './brief-list/brief-list.component';
import { DeleteBriefModalComponent } from './brief-list/delete-brief-modal.component';

import { BriefSelectionComponent } from './brief-selection/brief-selection.component';
import { BriefService } from './shared/brief.service';
import { PagerService } from './brief-list/pager.service';

@NgModule({
  imports: [
    NgbModalModule,
    SharedModule,
    BriefsRoutingModule
  ],
  declarations: [
    BriefsComponent,
    BriefListComponent,
    BriefSelectionComponent,
    DeleteBriefModalComponent
  ],
  providers: [
    BriefService, PagerService
  ],
  entryComponents: [
    DeleteBriefModalComponent
  ]
})
export class BriefsModule { }
