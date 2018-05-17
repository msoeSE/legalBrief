import { NgModule } from '@angular/core';

import { InitialRoutingModule } from './initial-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { InitialComponent } from './initial.component';
import { InitialExampleComponent } from './initial-example/initial-example.component';
import { InitialFormComponent } from './initial-form/initial-form.component';
import { InitialFinalComponent } from './initial-final/initial-final.component';

@NgModule({
  imports: [
    SharedModule,
    InitialRoutingModule
  ],
  declarations: [
    InitialComponent,
    InitialExampleComponent,
    InitialFormComponent,
    InitialFinalComponent
  ]
})
export class InitialModule { }
