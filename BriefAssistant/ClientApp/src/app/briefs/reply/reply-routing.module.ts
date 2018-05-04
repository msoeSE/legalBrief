import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ReplyComponent } from './reply.component';
import { ReplyExampleComponent } from './reply-example/reply-example.component';
import { ReplyFormComponent } from './reply-form/reply-form.component';
import { ReplyFinalComponent } from './reply-final/reply-final.component';

const intialRoutes: Routes = [
  {
    path: '',
    component: ReplyComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'example' },
      { path: 'example', component: ReplyExampleComponent },
      { path: ':id', component: ReplyFormComponent },
      { path: 'new', component: ReplyFormComponent },
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
