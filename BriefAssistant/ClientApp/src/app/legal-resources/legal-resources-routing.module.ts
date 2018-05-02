import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LegalResourcesComponent } from './legal-resources.component';

const legalResourcesRoutes: Routes = [{ path: '', component: LegalResourcesComponent }]

@NgModule({
  imports: [
    RouterModule.forChild(legalResourcesRoutes)
  ],
  exports: [RouterModule]
})
export class LegalResourcesRoutingModule { }
