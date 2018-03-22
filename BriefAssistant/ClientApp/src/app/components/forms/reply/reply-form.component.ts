import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { BriefInfo } from "../../../models/BriefInfo";
import { State } from "../../../models/State";
import { County } from "../../../models/County";
import { Role } from "../../../models/Role";
import { Observable } from 'rxjs/Observable';
import { BriefService } from "../../../services/brief.service";
import { ReplyBriefInfo } from "../../../models/ReplyBriefInfo";
import { BriefType } from "../../../models/BriefType";
import { ReplyBriefHolder } from "../../../models/ReplyBriefHolder";


@Component({
  selector: "reply-form",
  templateUrl: "./reply-form.component.html"
})

export class ReplyFormComponent implements OnInit{
  id: string | null;
  states = State;
  stateKeys = Object.keys(State);
  counties = County;
  countyKeys = Object.keys(County);
  roles = Role;
  roleKeys = Object.keys(Role);
  brief = new BriefInfo();
  replyInfo = new ReplyBriefInfo();

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
      this.briefService.getBrief(this.id)
        .subscribe(brief => {
          this.brief = brief;
          this.brief.contactInfo.address.state = (<any>State)[this.stateKeys[brief.contactInfo.address.state]];
          this.brief.circuitCourtCase.county = (<any>County)[this.countyKeys[brief.circuitCourtCase.county]];
          this.brief.circuitCourtCase.role = (<any>Role)[this.roleKeys[brief.circuitCourtCase.role]];
        });

      this.briefService
        .getReplyBrief(this.id)
        .subscribe(brief => {
          this.replyInfo = brief;
        });
    } else {
      this.brief = new BriefInfo();
      this.brief.type = BriefType.Reply;
      this.replyInfo = new ReplyBriefInfo();
    }
  }

  updateBrief() {
    this.saveBrief()
      .subscribe(brief => {
        this.brief.id = brief.briefInfo.id;
        this.replyInfo.id = brief.briefInfo.id;
        alert("Brief Saved!");
      });
  }

  saveBrief(): Observable<ReplyBriefHolder> {
    var holder = new ReplyBriefHolder();
    holder.briefInfo = this.brief;
    holder.replyBriefInfo = this.replyInfo;
    if (this.brief.id == null) {
      console.log(this.brief);
      return this.briefService.createReply(holder);
    } else {
      return this.briefService.updateReply(holder);
    }
  }

  finishBrief() {
    this.saveBrief()
      .subscribe(brief => {
        console.log(brief.briefInfo.id);
        this.brief.id = brief.briefInfo.id;
        this.replyInfo.id = brief.briefInfo.id;
        this.router.navigate(["/reply-final", brief.briefInfo.id]);
      });
  }
}
