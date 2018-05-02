import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { BriefsRoutingModule } from './briefs-routing.module';
import { SharedModule } from '../shared/shared.module';
import { BriefsComponent } from './briefs.component';
import { BriefListComponent } from './brief-list/brief-list.component';

import { BriefSelectionComponent } from './brief-selection/brief-selection.component';
import { BriefService } from './shared/brief.service';

@NgModule({
  imports: [
    SharedModule,
    HttpClientModule,
    BriefsRoutingModule
  ],
  declarations: [
    BriefsComponent,
    BriefListComponent,
    BriefSelectionComponent
  ],
  providers: [BriefService]
})
export class BriefsModule { }
