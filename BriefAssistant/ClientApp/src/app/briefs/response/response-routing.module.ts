import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ResponseComponent } from './response.component';
import { ResponseExampleComponent } from './response-example/response-example.component';
import { ResponseFormComponent } from './response-form/response-form.component';
import { ResponseFinalComponent } from './response-final/response-final.component';
import { PendingChangesGuard } from '../../core/warning/warning-guard';

const intialRoutes: Routes = [
  {
    path: '', component: ResponseComponent, children: [
      { path: '', pathMatch: 'full', redirectTo: 'example'},
      { path: 'example', component: ResponseExampleComponent },
      { path: 'new', component: ResponseFormComponent, canDeactivate: [PendingChangesGuard] },
      { path: ':id', component: ResponseFormComponent, canDeactivate: [PendingChangesGuard] },
      { path: ':id/final', component: ResponseFinalComponent }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(intialRoutes)
  ],
  exports: [RouterModule]
})
export class ResponseRoutingModule { }
