import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../core/auth-guard';
import { BriefsComponent } from './briefs.component';
import { BriefListComponent } from './brief-list/brief-list.component';
import { BriefSelectionComponent } from './brief-selection/brief-selection.component';

const briefsRoutes: Routes = [
  {
    path: '',
    component: BriefsComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      { path: '', component: BriefListComponent },
      { path: 'new', component: BriefSelectionComponent },
      {
        path: 'initial',
        loadChildren: './initial/initial.module#InitialModule'
      },
      {
        path: 'reply',
        loadChildren: './reply/reply.module#ReplyModule'
      },
      {
        path: 'response',
        loadChildren: './response/response.module#ResponseModule'
      }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(briefsRoutes)
  ],
  exports: [RouterModule]
})
export class BriefsRoutingModule {
}
