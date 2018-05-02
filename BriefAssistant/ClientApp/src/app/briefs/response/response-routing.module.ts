import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../core/auth-guard';
import { ResponseComponent } from './response.component';
import { ResponseExampleComponent } from './response-example/response-example.component';
import { ResponseFormComponent } from './response-form/response-form.component';
import { ResponseFinalComponent } from './response-final/response-final.component';

const intialRoutes: Routes = [
  {
    path: '', component: ResponseComponent, children: [
      { path: '', pathMatch: 'full', redirectTo: 'example'},
      { path: 'example', component: ResponseExampleComponent },
      { path: ':id', component: ResponseFormComponent, canActivate: [AuthGuard] },
      { path: 'new', component: ResponseFormComponent, canActivate: [AuthGuard] },
      { path: ':id/final', component: ResponseFinalComponent, canActivate: [AuthGuard] }
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
