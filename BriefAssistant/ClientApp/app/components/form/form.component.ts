import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { NgModule } from "@angular/core";
import { Router } from "@angular/router";

import { BrowserModule } from "@angular/platform-browser";
import { Http, Headers } from '@angular/http';


import { BriefInfo } from "../../models/BriefInfo";
import { State } from "../../models/State";
import { County } from "../../models/County";
import { Role } from "../../models/Role";
import { BriefGenerationResult } from "../../models/BriefGenerationResult";
import 'rxjs/add/operator/map';

@Component({
	selector: "dataForm",
	templateUrl: "./form.component.html"
})

export class FormComponent {
	states = State;
	stateKeys = Object.keys(State);
	counties = County;
	countyKeys = Object.keys(County);
	roles = Role;
	roleKeys = Object.keys(Role);
	model = new BriefInfo();


	constructor(
		readonly http: Http,
		readonly router: Router
	) { }

	onSubmit(form: NgForm) {
		var body = JSON.stringify(this.model);
		let headers = new Headers({ 'Content-Type': 'application/json' });

		this.http.post("/api/brief", body, { headers: headers })
			.map(res => res.json())
			.subscribe((data: BriefGenerationResult) => {
					this.router.navigate(["/final", data.id]);
				}
			);	
	}
}