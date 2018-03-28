import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { ResponseBriefInfo } from "../../../models/ResponseBriefInfo";
import { State } from "../../../models/State";
import { County } from "../../../models/County";
import { Role } from "../../../models/Role";
import { BriefService } from "../../../services/brief.service"

@Component({
	selector: "response-form",
	templateUrl: "./response-form.component.html"
})

export class ResponseFormComponent implements OnInit {
  id: string | null;
	states = State;
	stateKeys = Object.keys(State);
	counties = County;
	countyKeys = Object.keys(County);
	roles = Role;
	roleKeys = Object.keys(Role);
  responseInfo = new ResponseBriefInfo();

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
        .getResponseBrief(this.id)
        .subscribe(brief => {
            this.responseInfo.briefInfo.contactInfo.address.state =
              (<any>State)[this.stateKeys[brief.briefInfo.contactInfo.address.state]];
            this.responseInfo.briefInfo.circuitCourtCase.county =
              (<any>County)[this.countyKeys[brief.briefInfo.circuitCourtCase.county]];
            this.responseInfo.briefInfo.circuitCourtCase.role =
              (<any>Role)[this.roleKeys[brief.briefInfo.circuitCourtCase.role]];
            this.responseInfo = brief;
          },
          error => {
            this.router.navigate(["/**"]);
          });
    } else {
      this.responseInfo = new ResponseBriefInfo();
    }
  }

	updateBrief() {
	  this.saveBrief()
	    .subscribe(brief => {
	      this.responseInfo.id = brief.briefInfo.id;
	      alert("Brief Saved!");
	    });
	}

  private saveBrief(): Observable<ResponseBriefInfo> {
    if (this.responseInfo.id == null) {
      console.log(this.responseInfo);
      return this.briefService.createResponse(this.responseInfo);
    } else {
      return this.briefService.updateResponse(this.responseInfo);
		}
	}

	finishBrief() {
		this.saveBrief()
      .subscribe(brief => {
        console.log(brief.briefInfo.id);
        this.responseInfo.id = brief.id;
		    this.responseInfo.briefInfo.id = brief.briefInfo.id;
		    this.router.navigate(["/response-final", brief.briefInfo.id]);
		  });
	}
}
