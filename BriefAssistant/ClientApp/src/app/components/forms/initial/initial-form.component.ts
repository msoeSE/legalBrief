import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { BriefInfo } from "../../../models/BriefInfo";
import { InitialBriefInfo } from "../../../models/InitialBriefInfo";
import { State } from "../../../models/State";
import { County } from "../../../models/County";
import { Role } from "../../../models/Role";
import { BriefService } from "../../../services/brief.service"
import { BriefType } from "../../../models/BriefType";
import { InitialBriefHolder } from "../../../models/InitialBriefHolder";

@Component({
	selector: "initial-form",
	templateUrl: "./initial-form.component.html"
})

export class InitialFormComponent implements OnInit {
  private id: string | null;
	private states = State;
	private stateKeys = Object.keys(State);
	private counties = County;
	private countyKeys = Object.keys(County);
	private roles = Role;
	private roleKeys = Object.keys(Role);
  private brief = new BriefInfo();
  private initialInfo = new InitialBriefInfo();

	constructor(
		private readonly router: Router,
    private readonly briefService: BriefService,
		private route: ActivatedRoute
    ) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(this.id);
      
    //check lead Id here
    if (this.id !== null) {
      this.briefService.getBrief(this.id)
        .subscribe(brief => {
          this.brief = brief;
          this.brief.contactInfo.address.state =(<any>State)[this.stateKeys[brief.contactInfo.address.state]];
          this.brief.circuitCourtCase.county = (<any>County)[this.countyKeys[brief.circuitCourtCase.county]];
          this.brief.circuitCourtCase.role = (<any>Role)[this.roleKeys[brief.circuitCourtCase.role]];
        });

      this.briefService
        .getInitialBrief(this.id)
        .subscribe(brief => {
          this.initialInfo = brief;
        });
    } else {
      this.brief = new BriefInfo();
      this.brief.type = BriefType.Initial;
      this.initialInfo = new InitialBriefInfo();
    }
  }

	updateBrief() {
		this.saveBrief()
			.subscribe(() => alert("Brief Saved!"));
	}

  private saveBrief(): Observable<InitialBriefHolder> {
    var holder = new InitialBriefHolder();
    holder.briefInfo = this.brief;
    holder.initialBriefInfo = this.initialInfo;
    if (this.brief.id == null) {
      console.log(this.brief);
      return this.briefService.createInitial(holder);
    } else {
      return this.briefService.updateInitial(holder);
		}
	}

	finishBrief() {
		this.saveBrief()
      .subscribe(brief => {
        console.log(brief.briefInfo.id);
        this.brief.id = brief.briefInfo.id;
		    this.initialInfo.id = brief.briefInfo.id;
		    this.router.navigate(["/initial-final", brief.briefInfo.id]);
		  });
	}
}
