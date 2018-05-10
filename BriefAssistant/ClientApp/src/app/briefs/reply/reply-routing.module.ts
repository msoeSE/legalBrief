import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ReplyComponent } from './reply.component';
import { ReplyExampleComponent } from './reply-example/reply-example.component';
import { ReplyFormComponent } from './reply-form/reply-form.component';
import { ReplyFinalComponent } from './reply-final/reply-final.component';
import { PendingChangesGuard } from '../../core/warning/warning-guard';

const intialRoutes: Routes = [
  {
    path: '',
    component: ReplyComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'example' },
      { path: 'example', component: ReplyExampleComponent },
      { path: 'new', component: ReplyFormComponent, canDeactivate: [PendingChangesGuard] },
      { path: ':id', component: ReplyFormComponent, canDeactivate: [PendingChangesGuard] },
      { path: ':id/final', component: ReplyFinalComponent }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(intialRoutes)
  ],
  exports: [RouterModule]
})
export class ReplyRoutingModule {
}
