import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { IBriefList } from "../../models/IBriefList";
import { BriefService } from "../../services/brief.service";
import { IBriefListItem } from "../../models/IBriefListItem";

@Component({
    selector: "briefs",
    templateUrl: "./briefsList.component.html"
})

export class BriefsListComponent implements OnInit {
    private briefs : IBriefList;
    private list: IBriefListItem[];

    constructor(
        private readonly router: Router,
        private readonly briefService: BriefService
    ) { }

    ngOnInit() {
      this.briefService
        .getBriefList()
        .subscribe(briefList => {
          if (briefList != null) {
            this.briefs = briefList;
            this.list = this.briefs.briefs;
            console.log(this.briefs);
          }
        });
    }
    edit(id: string) {
        this.router.navigate(["/initial-form", id]);

    }
    delete(id: string, index: number) {
        this.briefs.briefs.splice(index, 1);
        //controller's delete method
    }
}
