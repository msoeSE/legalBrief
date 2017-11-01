import { Component } from '@angular/core';
import { HttpModule, Headers, Http, Response } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { DataService } from "../data.service";
import { ResponseContentType } from '@angular/http';
import 'rxjs/Rx';


@Component({
    selector: 'final',
    templateUrl: './final.component.html',
    styleUrls: ['./final.component.css']
})
export class FinalComponent {
    constructor(private data: DataService, private http: Http) { }

    briefID: string;
    ngOnInit() {
        this.data.getID().subscribe(id => this.briefID = id);
    }

    downloadBrief() {
        let headers = new Headers({ 'Accept': 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' });

        this.http.get("/api/brief/download/" + this.briefID, { headers: headers, responseType: ResponseContentType.Blob })
            .subscribe(
            data => {
                var mediaType = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';
                var blob = new Blob([data.blob()], { type: mediaType });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = "brief.docx";
                link.click();
            },
            error => {
                console.error(JSON.stringify(error.json()));
            })
    }

}
