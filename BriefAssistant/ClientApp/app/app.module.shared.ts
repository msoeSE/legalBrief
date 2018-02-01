import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { FormComponent } from './components/form/form.component';
import { FinalComponent } from './components/final/final.component';
import { WelcomeComponent } from './components/welcomes/welcome.component';
import { ExampleComponent } from './components/example/example.component';
import { InitialWelcomeComponent } from './components/welcomes/initial/initial.welcome.component';
import { ReplyWelcomeComponent } from './components/welcomes/reply/reply.welcome.component';
import { ResponseWelcomeComponent } from './components/welcomes/response/response.welcome.component';
import { PetitionWelcomeComponent } from './components/welcomes/petition/petition.welcome.component';

@NgModule({
    declarations: [
        AppComponent,
        FormComponent,
        FinalComponent,
        WelcomeComponent,
        ExampleComponent,
        InitialWelcomeComponent,
        ReplyWelcomeComponent,
        ResponseWelcomeComponent,
        PetitionWelcomeComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'welcome', component: WelcomeComponent},
            { path: '', redirectTo: '/welcome', pathMatch: 'full' },
            { path: 'final/:id', component: FinalComponent },
            { path: 'dataform', component: FormComponent },
            { path: 'example', component: ExampleComponent },
            { path: 'initial-welcome', component: InitialWelcomeComponent },
            { path: 'reply-welcome', component: ReplyWelcomeComponent },
            { path: 'response-welcome', component: ResponseWelcomeComponent },
            { path: 'petition-welcome', component: PetitionWelcomeComponent }
        ])
    ],
    exports: [RouterModule]

})
export class AppModuleShared {
}
