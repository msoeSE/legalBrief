import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { IBriefList } from "../../models/IBriefList";
import { BriefService } from "../../services/brief.service";
import { IBriefListItem } from "../../models/IBriefListItem";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BriefType } from "../../models/BriefType";
import { PagerService } from './services/pager.service'

@Component({
    selector: "briefs",
    templateUrl: "./briefsList.component.html"
})

export class BriefsListComponent implements OnInit {
    briefList : IBriefList;
    list: IBriefListItem[];

    constructor(
        private readonly router: Router,
        private readonly briefService: BriefService,
        private readonly http: HttpClient,
        private readonly pagerService: PagerService
    ) { }

  // pager object
  pager: any = {};

  // paged items
  pagedItems: IBriefListItem[];

    ngOnInit() {
      this.briefService
        .getBriefList()
        .subscribe(briefList => {
          if (briefList != null) {
            this.briefList = briefList;
            this.list = this.briefList.briefs;

            // initialize to page 1
            this.setPage(1);
          }
        });
    }

    setPage(page: number) {
      if (page < 1 || page > this.pager.totalPages) {
        return;
      }

      // get pager object from service
      this.pager = this.pagerService.getPager(this.list.length, page);

      // get current page of items
      this.pagedItems = this.list.slice(this.pager.startIndex, this.pager.endIndex + 1);
    }

    edit(id: string) {
      if (id !== null) {
        this.briefService.getBrief(id)
          .subscribe(brief => {
            switch (brief.type) {
              case BriefType.Initial:
                this.router.navigate(["/initial-form", id]);
                break;
              case BriefType.Reply:
                this.router.navigate(["/reply-form", id]);
                break;
              case BriefType.Response:
              case BriefType.Petition:
              default:
                this.router.navigate(["/**"]);
            }
          }, error => {
            this.router.navigate(["/**"]);
          });
      } else {
        this.router.navigate(["/**"]);
      }
    }
    delete(id: string, index: number) {
        this.briefList.briefs.splice(index, 1);
        //controller's delete method
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        this.http.post(`/api/briefs/${id}/delete`, { headers: headers })
          .subscribe(res => {
            alert("Delete successful");
          });
    }
}
