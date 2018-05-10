import { Component, OnInit, HostListener } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { State } from "../../shared/State";
import { County } from "../../shared/County";
import { Role } from "../../shared/Role";
import { Observable } from 'rxjs/Observable';
import { BriefService } from "../../shared/brief.service";
import { AuthService } from '../../../core/auth.service';
import { ReplyBriefInfo } from "../../shared/ReplyBriefInfo";
import { ComponentCanDeactivate } from "../../../core/warning/warning-guard";

@Component({
  selector: "replyForm",
  templateUrl: "./reply-form.component.html"
})

export class ReplyFormComponent implements OnInit, ComponentCanDeactivate {
  id: string | null;
  states = State;
  stateKeys = Object.keys(State);
  counties = County;
  countyKeys = Object.keys(County);
  roles = Role;
  roleKeys = Object.keys(Role);
  replyInfo = new ReplyBriefInfo();
  userType: string;

  constructor(
    readonly router: Router,
    readonly briefService: BriefService,
    private route: ActivatedRoute,
    private readonly authService: AuthService
  ) {
    this.userType = authService.userType.toString();
  }

  @HostListener('window:beforeunload')
  canDeactivate(): Observable<boolean> | boolean {
    return false;
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');

    if (this.id !== null) {
      this.briefService
        .getReplyBrief(this.id)
        .subscribe(brief => {
          this.replyInfo = brief;
          this.replyInfo.briefInfo.contactInfo.address.state =
            (<any>State)[this.stateKeys[brief.briefInfo.contactInfo.address.state]];
          this.replyInfo.briefInfo.circuitCourtCase.county =
            (<any>County)[this.countyKeys[brief.briefInfo.circuitCourtCase.county]];
          this.replyInfo.briefInfo.circuitCourtCase.role =
            (<any>Role)[this.roleKeys[brief.briefInfo.circuitCourtCase.role]];
        });
    } else {
      this.replyInfo = new ReplyBriefInfo();
    }
  }

  updateBrief() {
    this.saveBrief()
      .subscribe(brief => {
        this.replyInfo.briefInfo.id = brief.briefInfo.id;
        this.replyInfo.id = brief.briefInfo.id;
        alert("Brief Saved! If you want to edit this brief at a later time, you can find it by clicking the My Briefs tab in the navigation bar.");
      });
  }

  saveBrief(): Observable<ReplyBriefInfo> {
    if (this.replyInfo.id == null) {
      return this.briefService.createReply(this.replyInfo);
    } else {
      return this.briefService.updateReply(this.replyInfo);
    }
  }

  finishBrief() {
    this.saveBrief()
      .subscribe(brief => {
        this.replyInfo.briefInfo.id = brief.briefInfo.id;
        this.replyInfo.id = brief.briefInfo.id;
        this.router.navigate(["/briefs/reply", brief.briefInfo.id, "final"]);
      });
  }
}
