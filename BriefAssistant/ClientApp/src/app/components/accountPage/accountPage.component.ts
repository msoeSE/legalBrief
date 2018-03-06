import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { Http, Headers } from "@angular/http";
import { State } from "../../models/State";

@Component({
    selector: "accountPage",
    templateUrl: "./accountPage.component.html",
})
export class AccountPageComponent {
    stateKeys = Object.keys(State);
    onSubmit(form: NgForm) {

    }
}
