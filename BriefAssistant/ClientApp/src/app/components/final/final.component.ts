import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from "@angular/forms";
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { EmailRequest } from "../../models/EmailRequest";

@Component({
    selector: 'final',
    templateUrl: './final.component.html'
})
export class FinalComponent {
	private id: string | null;

    constructor(
        private readonly http: HttpClient,
		private route: ActivatedRoute
	){}

    private model = new EmailRequest();

    onSubmit(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        this.http.post("/api/brief/email/" + this.id, body, { headers: headers })
          .subscribe(res => {
            alert("Email Sent!");
        });
    };

	ngOnInit() {
		this.id = this.route.snapshot.paramMap.get('id');
	}
}
