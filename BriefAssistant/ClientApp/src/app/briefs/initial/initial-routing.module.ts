import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../core/auth-guard';
import { InitialComponent } from './initial.component';
import { InitialExampleComponent } from './initial-example/initial-example.component';
import { InitialFormComponent } from './initial-form/initial-form.component';
import { InitialFinalComponent } from './initial-final/initial-final.component';

const intialRoutes: Routes = [
  {
    path: '',
    component: InitialComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'example' },
      { path: 'example', component: InitialExampleComponent },
      { path: ':id', component: InitialFormComponent, canActivate: [AuthGuard] },
      { path: 'new', component: InitialFormComponent, canActivate: [AuthGuard] },
      { path: ':id/final', component: InitialFinalComponent, canActivate: [AuthGuard] }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(intialRoutes)
  ],
  exports: [RouterModule]
})
export class InitialRoutingModule {
}
