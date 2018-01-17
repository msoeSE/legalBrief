import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { FormComponent } from './components/form/form.component';
import { FinalComponent } from './components/final/final.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { ExampleComponent } from './components/example/example.component';
import { briefsListComponent } from './components/briefsList/briefsList.component';

@NgModule({
    declarations: [
        AppComponent,
        FormComponent,
        FinalComponent,
        WelcomeComponent,
        ExampleComponent,
        briefsListComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'welcome', component: WelcomeComponent},
            { path: '', redirectTo: '/welcome', pathMatch: 'full' },
            { path: 'final/:id', component: FinalComponent },
            { path: 'dataform/:id', component: FormComponent },
            { path: 'dataform', component: FormComponent },
            { path: 'example', component: ExampleComponent },
            { path: 'briefs', component: briefsListComponent },
        ])
    ],
    exports: [RouterModule]

})
export class AppModuleShared {
}
