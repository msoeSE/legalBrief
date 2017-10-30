import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { NgModule } from "@angular/core";
import { Router } from "@angular/router";

import { HttpModule, Headers, Http} from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";

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


	constructor(private readonly http: Http, private readonly router: Router) {
	}

	onSubmitTemplateBased(form: NgForm) {
		var body = JSON.stringify(this.model);
		let headers = new Headers({ 'Content-Type': 'application/json' });

		console.log(body);

		this.http.post("/api/brief", body, {headers: headers}).subscribe(
			data => {
				console.log(data);
			},
			error => {
				console.error(JSON.stringify(error.json()));
			}
		);
		this.router.navigateByUrl("/final");
	}
}