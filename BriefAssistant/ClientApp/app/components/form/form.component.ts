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
            .subscribe((data: BriefInfo) => {
                this.model = data;
                var countyVal: County = (<any>County)[this.countyKeys[parseInt(data.circuitCourtCase.county.toString())]];
                alert(data.circuitCourtCase.county);
                this.model.circuitCourtCase.county = countyVal;
                var stateVal: State = (<any>State)[this.stateKeys[parseInt(data.appellant.address.state.toString())]];
                this.model.appellant.address.state = stateVal;
                alert(State[stateVal]);                              
                this.model.appellant.address.state = (<any>State)[this.stateKeys[data.Appellant.Address.State]];
            });
    }
    updateCounty(county: County) {
        this.model.circuitCourtCase.county = county;
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