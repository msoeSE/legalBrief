import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from "@angular/forms";
import { Http, Headers } from '@angular/http';

import { Email } from "../../models/Email";

@Component({
    selector: 'final',
    templateUrl: './final.component.html'
})
export class FinalComponent {
	private id: string | null;

    constructor(
        private readonly http: Http,
		private route: ActivatedRoute
	){}

    private model: Email = new Email();

    onSubmit(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new Headers({ 'Content-Type': 'application/json' });

        this.http.post("/api/brief/email/" + this.id, body, { headers: headers }).subscribe(data => {
            alert("Email Sent!");
        });
    };

	ngOnInit() {
		this.id = this.route.snapshot.paramMap.get('id');
	}
}
