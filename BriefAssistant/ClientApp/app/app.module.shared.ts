import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { FormComponent } from './components/form/form.component';
import { FinalComponent } from './components/final/final.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { ExampleComponent } from './components/example/example.component';
import { DataService } from "./components/data.service";

@NgModule({
    declarations: [
        AppComponent,
        FormComponent,
        FinalComponent,
        WelcomeComponent,
        ExampleComponent
    ],
    providers: [
        DataService
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'welcome', component: WelcomeComponent},
            { path: '', redirectTo: '/welcome', pathMatch: 'full' },
            { path: 'final', component: FinalComponent },
            { path: 'dataform', component: FormComponent },
            { path: 'example', component: ExampleComponent },
        ])
    ],
    exports: [RouterModule]

})
export class AppModuleShared {
}
