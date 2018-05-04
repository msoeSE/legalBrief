import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

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
      { path: ':id', component: InitialFormComponent },
      { path: 'new', component: InitialFormComponent },
      { path: ':id/final', component: InitialFinalComponent }
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
