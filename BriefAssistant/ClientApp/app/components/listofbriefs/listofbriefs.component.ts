import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Http, Headers } from '@angular/http';
import { Brief} from "../../models/Brief";

@Component({
    selector: "briefs",
    templateUrl: "./listofbriefs.component.html"
})

export class ListOfBriefsComponent implements OnInit {
    private briefs : Brief[];

    constructor(
        private readonly http: Http,
        private readonly router: Router
    ) { }

    ngOnInit() {
        let headers = new Headers({ 'Content-Type': 'application/json' });

        this.http.get("/api/briefs/",  { headers: headers })
            .map(res => res.json())
            .subscribe((data: Brief[]) => {
                this.briefs = data;
            }
            );
    }
    edit(id: String) {
        this.router.navigate(["/dataform", id]);

    }
}