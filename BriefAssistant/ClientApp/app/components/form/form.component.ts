import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Http, Headers } from '@angular/http';
import { BriefInfo } from "../../models/BriefInfo";
import { State } from "../../models/State";
import { County } from "../../models/County";
import { Role } from "../../models/Role";
import { BriefService } from "../../services/brief.service"

import 'rxjs/add/operator/map';

@Component({
	selector: "dataForm",
	templateUrl: "./form.component.html"
})

export class FormComponent implements OnInit {
	private states = State;
	private stateKeys = Object.keys(State);
	private counties = County;
	private countyKeys = Object.keys(County);
	private roles = Role;
	private roleKeys = Object.keys(Role);
	private brief = new BriefInfo();

	private headers = new Headers({ 'Content-Type': "application/json" });

	constructor(
		private readonly http: Http,
		private readonly router: Router,
		private readonly briefService: BriefService
    ) { }

	ngOnInit() {
		this.briefService
			.getBriefList()
			.then(briefList => {
				this.briefService.getBrief(briefList.briefs[0].id)
					.then(brief => this.brief = brief);
			});
	}

	updateBrief() {
		this.saveBrief()
			.then(() => alert("Brief Saved!"));
	}

	private saveBrief() : Promise<BriefInfo> {
		if (this.brief.id == null) {
			return this.briefService.create(this.brief);
		} else {
			return this.briefService.update(this.brief);
		}
	}

	finishBrief(form: NgForm) {
		this.saveBrief()
			.then(brief => this.router.navigate(["/final", brief.id]));
	}
}