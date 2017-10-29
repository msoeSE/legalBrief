import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { NgModule } from "@angular/core";
import { Router } from "@angular/router";

import { HttpModule, Headers, Http} from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";

interface BriefInfo {
	appellant: Appellant;
	issuesPresented: string;
	oralArgumentStatement: string;
	publicationStatement: string;
	caseFactsStatement: string;
	argument: string;
	conclusion: string;
	appendexDocuments: string;
	circuitCourtCase: CircuitCourtCase;
}

interface Appellant {
	name: string;
	address: Address;
	email: string;
	phone: string;
}

export enum State {
	AK,
	AL,
	AR,
	AZ,
	CA,
	CO,
	CT,
	DC,
	DE,
	FL,
	GA,
	HI,
	IA,
	ID,
	IL,
	IN,
	KS,
	KY,
	LA,
	MA,
	MD,
	ME,
	MI,
	MN,
	MO,
	MS,
	MT,
	NC,
	ND,
	NE,
	NH,
	NJ,
	NM,
	NV,
	NY,
	OH,
	OK,
	OR,
	PA,
	RI,
	SC,
	SD,
	TN,
	TX,
	UT,
	VA,
	VT,
	WA,
	WI,
	WV,
	WY
}

enum County {
	Adams,
	Ashland,
	Barron,
	Bayfield,
	Brown,
	Buffalo,
	Burnett,
	Calumet,
	Chippewa,
	Clark,
	Columbia,
	Crawford,
	Dane,
	Dodge,
	Door,
	Douglas,
	Dunn,
	EauClaire,
	Florence,
	FondDuLac,
	Forest,
	Grant,
	Green,
	GreenLake,
	Iowa,
	Iron,
	Jackson,
	Jefferson,
	Juneau,
	Kenosha,
	Kewaunee,
	LaCrosse,
	Lafayette,
	Langlade,
	Lincoln,
	Manitowoc,
	Marathon,
	Marinette,
	Marquette,
	Menominee,
	Milwaukee,
	Monroe,
	Oconto,
	Oneida,
	Outagamie,
	Ozaukee,
	Pepin,
	Pierce,
	Polk,
	Portage,
	Price,
	Racine,
	Richland,
	Rock,
	Rusk,
	Sauk,
	Sawyer,
	Shawano,
	Sheboygan,
	StCroix,
	Taylor,
	Trempealeau,
	Vernon,
	Vilas,
	Walworth,
	Washburn,
	Washington,
	Waukesha,
	Waupaca,
	Waushara,
	Winnebago,
	Wood
}

enum Role {
	Plantiff,
	Defendent
}

interface CircuitCourtCase {
	county: County;
	caseNumber: string;
	role: Role;
	judgeFirstName: string;
	judgeLastName: string;
	opponentName: string;
}

interface Address {
	street: string;
	street2: string;
	city: string;
	state: State;
	zip: string;
}

@NgModule({
	imports: [
		BrowserModule,
		HttpModule
	]
})

@Component({
	selector: "dataForm",
	templateUrl: "./form.component.html",
	styleUrls: ["./form.component.css"]
})

export class FormComponent {
	briefInfo: BriefInfo;
	appellant: Appellant;
	address: Address;
	circuitCourtCase: CircuitCourtCase;

	ngOnInit() {
		//initialize form
		this.address = {
			street: "",
			street2: "",
			city: "",
			state: State.WI,
			zip: ""
		};

		this.appellant = {
			name: "",
			address: this.address,
			email: "",
			phone: ""
		};

		this.circuitCourtCase = {
			county: County.Milwaukee,
			caseNumber: "",
			role: Role.Defendent,
			judgeFirstName: "",
			judgeLastName: "",
			opponentName: ""
		};

		this.briefInfo = {
			appellant: this.appellant,
			issuesPresented: "",
			oralArgumentStatement: "",
			publicationStatement: "",
			caseFactsStatement: "",
			argument: "",
			conclusion: "",
			appendexDocuments: "",
			circuitCourtCase: this.circuitCourtCase
		};
	}

	constructor(private http: Http, private router: Router) {}

	onSubmitTemplateBased(form: NgForm) {
		var body = JSON.stringify(this.briefInfo);
		let headers = new Headers({ 'Content-Type': 'application/json' });

		this.http.post("/api/brief", body, {headers: headers}).subscribe(
			data => {
				console.log(data);
			},
			error => {
				console.error(JSON.stringify(error.json()));
			}
		);
		this.router.navigateByUrl("/final");
	}
}