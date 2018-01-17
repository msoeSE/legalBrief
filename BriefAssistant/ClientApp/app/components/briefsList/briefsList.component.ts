import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Http, Headers } from '@angular/http';
import { IBriefList } from "../../models/IBriefList";

@Component({
    selector: "briefs",
    templateUrl: "./briefsList.component.html"
})

export class briefsListComponent implements OnInit {
    private briefs : IBriefList;

    constructor(
        private readonly http: Http,
        private readonly router: Router
    ) { }

    ngOnInit() {

        this.http.get("/api/briefs/")
            .map(res => res.json())
            .subscribe((data: IBriefList) => {
                this.briefs = data;
            }
            );
    }
    edit(id: String) {
        this.router.navigate(["/dataform", id]);

    }
}