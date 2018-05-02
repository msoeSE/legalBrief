import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { EmailRequest } from '../../../shared/EmailRequest';

@Component({
    selector: 'replyFinal',
    templateUrl: './reply-final.component.html'
})
export class ReplyFinalComponent {
    private id: string | null;

    constructor(
      readonly router: Router,
      private readonly http: HttpClient,
      private route: ActivatedRoute
    ) { }

    model = new EmailRequest();

    download() {
      let headers = new HttpHeaders({ 'Accept': 'application/octet-stream' });
      this.http.get(`/api/briefs/${this.id}/download`, { headers: headers, responseType: 'blob' }).subscribe(res => {
        const url = window.URL.createObjectURL(res);

        // create hidden dom element (so it works in all browsers)
        const a = document.createElement('a');
        a.setAttribute('style', 'display:none;');
        document.body.appendChild(a);

        // create file, attach to hidden element and open hidden element
        a.href = url;
        a.download = 'replybrief.docx';
        a.click();
        return url;
      });
    }

    onSubmit(form: NgForm) {
      var body = JSON.stringify(this.model);
      let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

      this.http.post(`/api/briefs/${this.id}/email`, body, { headers: headers })
        .subscribe(res => {
          alert("Email Sent!");
        });
    };

    ngOnInit() {
        this.id = this.route.snapshot.paramMap.get('id');
    }

    backToForm() {
      this.router.navigate(["/briefs/reply", this.id]);
    }
}
