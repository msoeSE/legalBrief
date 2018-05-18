import { Component, HostListener } from '@angular/core';
import { OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { InitialBriefInfo } from '../../shared/InitialBriefInfo';
import { State } from "../../shared/State";
import { County } from "../../shared/County";
import { Role } from "../../shared/Role";
import { BriefService } from "../../shared/brief.service"
import { AuthService } from '../../../core/auth.service';
import { ComponentCanDeactivate } from '../../../core/warning-guard';

@Component({
  selector: "initialForm",
  templateUrl: "./initial-form.component.html"
})

export class InitialFormComponent implements OnInit, ComponentCanDeactivate {
  id: string | null;
  states = State;
  stateKeys = Object.keys(State);
  counties = County;
  countyKeys = Object.keys(County);
  roles = Role;
  roleKeys = Object.keys(Role);
  initialInfo = new InitialBriefInfo();
  userType: string;

  constructor(
    readonly router: Router,
    readonly briefService: BriefService,
    private route: ActivatedRoute,
    private readonly  authService: AuthService
  ) {
    this.userType = authService.userType.toString();
  }

  @HostListener('window:beforeunload')
  canDeactivate(): Observable<boolean> | boolean {
    return false;
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');

    //check lead Id here
    if (this.id !== null) {
      this.briefService
        .getInitialBrief(this.id)
        .subscribe(brief => {
          this.initialInfo = brief;
          this.initialInfo.briefInfo.contactInfo.address.state =
            this.stateKeys[brief.briefInfo.contactInfo.address.state];
          this.initialInfo.briefInfo.circuitCourtCase.county =
            this.countyKeys[brief.briefInfo.circuitCourtCase.county];
          this.initialInfo.briefInfo.circuitCourtCase.role =
            (<any>Role)[this.roleKeys[brief.briefInfo.circuitCourtCase.role]];

        },
        error => {
          this.router.navigate(["/**"]);
        });
    } else {
      this.initialInfo = new InitialBriefInfo();
    }
  }

  updateBrief() {
    this.saveBrief()
      .subscribe(brief => {
        this.initialInfo.briefInfo.id = brief.briefInfo.id;
        this.initialInfo.id = brief.briefInfo.id;
        alert("Brief Saved! If you want to edit this brief at a later time, you can find it by clicking the My Briefs tab in the navigation bar.");
      });
  }

  saveBrief(): Observable<InitialBriefInfo> {
    if (this.initialInfo.id == null) {
      return this.briefService.createInitial(this.initialInfo);
    } else {
      return this.briefService.updateInitial(this.initialInfo);
    }
  }

  finishBrief() {
    this.saveBrief()
      .subscribe(brief => {
        this.initialInfo.id = brief.id;
		    this.initialInfo.briefInfo.id = brief.briefInfo.id;
		    this.router.navigate(["/briefs/initial", brief.briefInfo.id, "final"]);
		  });
	}
}
