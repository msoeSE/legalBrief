import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { NgModule, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { BrowserModule } from "@angular/platform-browser";
import { Http, Headers } from '@angular/http';


import { BriefInfo } from "../../models/BriefInfo";
import { State } from "../../models/State";
import { County } from "../../models/County";
import { Role } from "../../models/Role";
import { BriefGenerationResult } from "../../models/BriefGenerationResult";
import 'rxjs/add/operator/map';

@Component({
	selector: "dataForm",
	templateUrl: "./form.component.html"
})

export class FormComponent implements OnInit {
	private states = State;
	private stateKeys = Object.keys(State);
	private counties = County;
	private countyKeys = Object.keys(County);
	private roles = Role;
	private roleKeys = Object.keys(Role);
	private model = new BriefInfo();


	constructor(
		private readonly http: Http,
		private readonly router: Router
    ) { }
    ngOnInit() {
        this.retrieve();
        
    }
    save(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.post("/api/data/save", body, { headers: headers })
            .map(res => res.json())
            .subscribe(data => {               
                alert("create Sent!");
            });


    }
    retrieve() {
        var body = JSON.stringify({ Email: "kungm@msoe.edu" });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.http.post("/api/data/retrieve", body, { headers: headers })
            .map(res => res.json())
            .subscribe(data => {
                this.model.appellant.address.city = String(data.Appellant.Address.City);
                this.model.appellant.address.state = State[data.Appellant.Address.State as keyof typeof State];

                this.model.appellant.address.street = String(data.Appellant.Address.Street);

                this.model.appellant.address.street2 = String(data.Appellant.Address.Street2);
                this.model.appellant.address.zip = String(data.Appellant.Address.Zip);
                this.model.appellant.email = String(data.Appellant.Email);
                this.model.appellant.name = String(data.Appellant.Name);
                this.model.appellant.phone = String(data.Appellant.Phone);
                alert(data.Appellant.Address.State);

                this.model.appendexDocuments = String(data.AppendexDocuments);
                this.model.argument = String(data.Argument);
                this.model.caseFactsStatement = String(data.CaseFactsStatement);
                this.model.circuitCourtCase.caseNumber = String(data.CircuitCourtCase.CaseNumber);
                this.model.circuitCourtCase.county = County[data.CircuitCourtCase.County as keyof typeof County];

                this.model.circuitCourtCase.judgeFirstName = String( data.CircuitCourtCase.JudgeFirstName);

                this.model.circuitCourtCase.judgeLastName = String(data.CircuitCourtCase.JudgeLastName);
                this.model.circuitCourtCase.role = Role[data.CircuitCourtCase.Role as keyof typeof Role];

                this.model.circuitCourtCase.opponentName = String( data.CircuitCourtCase.OpponentName);



                this.model.conclusion = String(data.Conclusion);
                this.model.issuesPresented = String(data.IssuesPresented);
                this.model.oralArgumentStatement = String(data.OralArgumentStatement);
                this.model.publicationStatement = String(data.PublicationStatement);
            });


    }


    generateBrief(form: NgForm) {
        var body = JSON.stringify(this.model);
        let headers = new Headers({ 'Content-Type': 'application/json' });
		
        this.http.post("/api/brief", body, { headers: headers })
            .map(res => res.json())
            .subscribe((data: BriefGenerationResult) => {
                this.save(form);
                this.router.navigate(["/final", data.id]);
            });	
        
    }

}