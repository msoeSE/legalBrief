import { Component, HostListener } from '@angular/core';
import { OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { ResponseBriefInfo } from "../../shared/ResponseBriefInfo";
import { State } from "../../shared/State";
import { County } from "../../shared/County";
import { Role } from "../../shared/Role";
import { BriefService } from "../../shared/brief.service";
import { AuthService } from '../../../core/auth.service';
import { ComponentCanDeactivate } from '../../../core/warning/warning-guard';

@Component({
	selector: "response-form",
	templateUrl: "./response-form.component.html"
})

export class ResponseFormComponent implements OnInit, ComponentCanDeactivate {
  id: string | null;
	states = State;
	stateKeys = Object.keys(State);
	counties = County;
	countyKeys = Object.keys(County);
	roles = Role;
	roleKeys = Object.keys(Role);
  responseInfo = new ResponseBriefInfo();
  userType: string;

	constructor(
		readonly router: Router,
		readonly briefService: BriefService,
    private route: ActivatedRoute,
    private readonly authService: AuthService
  ) { this.userType = authService.userType.toString(); }

  @HostListener('window:beforeunload')
  canDeactivate(): Observable<boolean> | boolean {
    return false;
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
      
    //check lead Id here
    if (this.id !== null) {
      this.briefService
        .getResponseBrief(this.id)
        .subscribe(brief => {
            this.responseInfo = brief;
            this.responseInfo.briefInfo.contactInfo.address.state =
              (<any>State)[this.stateKeys[brief.briefInfo.contactInfo.address.state]];
            this.responseInfo.briefInfo.circuitCourtCase.county =
              (<any>County)[this.countyKeys[brief.briefInfo.circuitCourtCase.county]];
            this.responseInfo.briefInfo.circuitCourtCase.role =
              (<any>Role)[this.roleKeys[brief.briefInfo.circuitCourtCase.role]];
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
	      this.responseInfo.briefInfo.id = brief.briefInfo.id;
	      this.responseInfo.id = brief.briefInfo.id;
	      alert("Brief Saved!");
	    });
	}

  private saveBrief(): Observable<ResponseBriefInfo> {
    if (this.responseInfo.id == null) {
      return this.briefService.createResponse(this.responseInfo);
    } else {
      return this.briefService.updateResponse(this.responseInfo);
		}
	}

	finishBrief() {
		this.saveBrief()
      .subscribe(brief => {
        this.responseInfo.id = brief.id;
		    this.responseInfo.briefInfo.id = brief.briefInfo.id;
		    this.router.navigate(["/briefs/response", brief.briefInfo.id, "final"]);
		  });
	}
}
