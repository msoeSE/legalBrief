import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { NgModule } from "@angular/core";
import { Router } from "@angular/router";

import { HttpModule, Headers, Http} from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { DataService } from "../data.service";

import { BriefInfo } from "../../models/BriefInfo";
import { State } from "../../models/State";
import { County } from "../../models/County";
import {Role} from "../../models/Role";

@NgModule({
	imports: [
		BrowserModule,
		HttpModule
	]
})

@Component({
	selector: "dataForm",
	templateUrl: "./form.component.html",
	styleUrls: ["./form.component.css"]
})

export class FormComponent {
	private states = State;
	private stateKeys = Object.keys(State);
	private counties = County;
	private countyKeys = Object.keys(County);
	private roles = Role;
	private roleKeys = Object.keys(Role);
	private model = new BriefInfo();
    private briefID: string;

    ngOnInit() {
        this.data.getID().subscribe(id => this.briefID = id);
    }

    constructor(private readonly http: Http, private readonly router: Router, private data: DataService) {
    }

	onSubmitTemplateBased(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.post("/api/brief", body, { headers: headers }).subscribe(
            data => {
                var jsonRes = data.json();
                if (data.status === 201) {
                    this.briefID = jsonRes.id;
                    this.data.setID(this.briefID);
                    this.router.navigateByUrl("/final");
                }
            },
            error => {
                console.error(JSON.stringify(error.json()));
            }
		);
		this.router.navigateByUrl("/final");
	}
}