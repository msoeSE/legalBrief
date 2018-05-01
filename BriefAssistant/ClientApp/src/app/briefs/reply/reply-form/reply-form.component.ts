import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { BriefInfo } from "../../../models/BriefInfo";
import { State } from "../../../models/State";
import { County } from "../../../models/County";
import { Role } from "../../../models/Role";
import { Observable } from 'rxjs/Observable';
import { BriefService } from "../../../services/brief.service";
import { AccountService } from '../../../services/account.service';
import { ReplyBriefInfo } from "../../../models/ReplyBriefInfo";

@Component({
  selector: "reply-form",
  templateUrl: "./reply-form.component.html"
})

export class ReplyFormComponent implements OnInit {
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
    public accountService: AccountService
  ) {
    this.userType = accountService.userType.toString();
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
        alert("Brief Saved!");
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
        this.router.navigate(["/reply-final", brief.briefInfo.id]);
      });
  }
}
