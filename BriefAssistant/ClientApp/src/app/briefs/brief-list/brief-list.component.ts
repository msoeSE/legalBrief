import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { IBriefList } from "../shared/IBriefList";
import { BriefService } from "../shared/brief.service";
import { IBriefListItem } from "../shared/IBriefListItem";
import { BriefType } from "../shared/BriefType";
import { PagerService } from './pager.service'

@Component({
    selector: "briefList",
    templateUrl: "./brief-list.component.html"
})
export class BriefListComponent implements OnInit {
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

    edit(item: IBriefListItem) {
      switch (item.type) {
      case BriefType.Initial:
        this.router.navigate(["/briefs/initial", item.id]);
        break;
      case BriefType.Reply:
        this.router.navigate(["/briefs/reply", item.id]);
        break;
      case BriefType.Response:
        this.router.navigate(["/briefs/response", item.id]);
        break;
      case BriefType.Petition:
      default:
        this.router.navigate(["/**"]);
      }
    }
    delete(id: string, index: number) {
        this.briefList.briefs.splice(index, 1);
        this.pagedItems.splice(index, 1);
        //controller's delete method
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        this.http.post(`/api/briefs/${id}/delete`, { headers: headers })
          .subscribe(res => {
            alert("Delete successful");
          });
    }
}
