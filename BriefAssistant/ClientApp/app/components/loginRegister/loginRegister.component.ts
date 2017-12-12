import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from "@angular/forms";
import { Http, Headers } from '@angular/http';
import { LoginRequest } from "../../models/LoginRequest";
import {RegistrationRequest} from "../../models/RegistrationRequest";

@Component({
    selector: "loginRegister",
    templateUrl: "./loginRegister.component.html",
})
export class LoginRegisterComponent {
    private loginModel = new LoginRequest();
    private registerModel = new RegistrationRequest();
   
    onLoginSubmit(form: NgForm) {
	    
    }

    onRegisterSubmit(form: NgForm) {
        
    }
}