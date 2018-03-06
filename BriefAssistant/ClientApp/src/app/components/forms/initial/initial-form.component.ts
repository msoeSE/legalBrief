import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { tap } from 'rxjs/operators';

import { BriefInfo } from "../../../models/BriefInfo";
import { State } from "../../../models/State";
import { County } from "../../../models/County";
import { Role } from "../../../models/Role";
import { BriefService } from "../../../services/brief.service"

@Component({
	selector: "initial-form",
	templateUrl: "./initial-form.component.html"
})

export class InitialFormComponent implements OnInit {
	states = State;
	stateKeys = Object.keys(State);
	counties = County;
	countyKeys = Object.keys(County);
	roles = Role;
	roleKeys = Object.keys(Role);
	brief = new BriefInfo();

	constructor(
		readonly router: Router,
		readonly briefService: BriefService
    ) { }

	ngOnInit() {
		this.briefService
			.getBriefList()
			.subscribe(briefList => {
				console.log(briefList);
				if (briefList.briefs.length !== 0) {
					this.briefService.getBrief(briefList.briefs[0].id)
            .subscribe(brief => {
					    console.log(brief);
              this.brief = brief;
					  });
				} else {
					this.brief = new BriefInfo();
				}
			});
	}

	updateBrief() {
		this.saveBrief()
			.subscribe(() => alert("Brief Saved!"));
	}

	private saveBrief() : Observable<BriefInfo> {
    if (this.brief.id == null) {
      console.log(this.brief);
      return this.briefService.create(this.brief);
    } else {
			return this.briefService.update(this.brief);
		}
	}

	finishBrief() {
		this.saveBrief()
      .subscribe(brief => {
        console.log(brief.id);
		    this.brief.id = brief.id;
		    this.router.navigate(["/initial-final", brief.id]);
		  });
	}
}
