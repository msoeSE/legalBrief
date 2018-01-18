import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { FormComponent } from './components/form/form.component';
import { FinalComponent } from './components/final/final.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { ExampleComponent } from './components/example/example.component';
import { LoginRegisterComponent } from "./components/loginRegister/loginRegister.component";
import { AccountPageComponent } from "./components/accountPage/accountPage.component";
import { ForgotPasswordComponent } from "./components/forgotPassword/forgotPassword.component";
import { ResetPasswordComponent } from "./components/resetPassword/resetPassword.component";
import { PasswordValidator } from "./passwordValidator.directive";
import { BriefService } from './services/brief.service';

@NgModule({
    declarations: [
        AppComponent,
        FormComponent,
        FinalComponent,
        WelcomeComponent,
        ExampleComponent,
        LoginRegisterComponent,
        AccountPageComponent,
        ForgotPasswordComponent,
		ResetPasswordComponent,
        PasswordValidator
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'welcome', component: WelcomeComponent },
            { path: '', redirectTo: '/welcome', pathMatch: 'full' },
            { path: 'final/:id', component: FinalComponent },
            { path: 'dataform', component: FormComponent },
            { path: 'example', component: ExampleComponent },
            { path: 'loginRegister', component: LoginRegisterComponent },
            { path: 'accountPage', component: AccountPageComponent },
            { path: 'forgotPassword', component: ForgotPasswordComponent },
            { path: 'resetPassword', component: ResetPasswordComponent }
        ])
    ],
	exports: [RouterModule],
	providers: [BriefService]

})
export class AppModuleShared {
}
