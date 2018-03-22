import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { OnInit } from '@angular/core';
import { Router,ActivatedRoute } from '@angular/router';
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
  id: string | null;
	states = State;
	stateKeys = Object.keys(State);
	counties = County;
	countyKeys = Object.keys(County);
	roles = Role;
	roleKeys = Object.keys(Role);
	brief = new BriefInfo();

	constructor(
		readonly router: Router,
		readonly briefService: BriefService,
    private route: ActivatedRoute
    ) { }

    ngOnInit() {
      this.id = this.route.snapshot.paramMap.get('id');
      console.log(this.id);
     
        //check lead Id here
      if (this.id !== null) {
          this.briefService
            .getBrief(this.id)
                .subscribe(brief => {
                  this.brief = brief;
                  this.brief.contactInfo.address.state =(<any>State)[this.stateKeys[brief.contactInfo.address.state]];
                  this.brief.circuitCourtCase.county = (<any>County)[this.countyKeys[brief.circuitCourtCase.county]];
                  this.brief.circuitCourtCase.role = (<any>Role)[this.roleKeys[brief.circuitCourtCase.role]];
                }, error => {
                  this.router.navigate(["/**"]);
                });
        } else {
              this.brief = new BriefInfo();
        }
      
    }

	updateBrief() {
		this.saveBrief()
			.subscribe(() => alert("Brief Saved!"));
	}

	saveBrief() : Observable<BriefInfo> {
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
