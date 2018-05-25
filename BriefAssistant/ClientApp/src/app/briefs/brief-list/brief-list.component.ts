import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { IBriefList } from "../shared/IBriefList";
import { BriefService } from "../shared/brief.service";
import { IBriefListItem } from "../shared/IBriefListItem";
import { BriefType } from "../shared/BriefType";
import { PagerService } from './pager.service';
import { DeleteBriefModalComponent } from './delete-brief-modal.component';

@Component({
    templateUrl: "./brief-list.component.html"
})
export class BriefListComponent implements OnInit {
  briefType = BriefType;
  briefList: IBriefList;
  list: IBriefListItem[];

  constructor(
    private readonly modalService: NgbModal,
    private readonly router: Router,
    private readonly briefService: BriefService,
    private readonly http: HttpClient,
    private readonly pagerService: PagerService
  ) {
  }

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

  delete(item: IBriefListItem, index: number) {
    const modalRef = this.modalService.open(DeleteBriefModalComponent);
    modalRef.componentInstance.briefName = item.title;
    modalRef.result.then(() => {
      this.briefList.briefs.splice(index, 1);
      this.pagedItems.splice(index, 1);
      //controller's delete method
      let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

      this.http.post(`/api/briefs/${item.id}/delete`, { headers: headers })
        .subscribe(res => {
          alert("Delete successful");
        });
    }, () => {}); // do nothing on dismissial
  }
}
