import { Appellant } from "./Appellant";
import { CircuitCourtCase } from "./CircuitCourtCase";

export class BriefInfo {
	circuitCourtCase: CircuitCourtCase;
	appendexDocuments: string;
	conclusion: string;
	caseFactsStatement: string;
	argument: string;
	oralArgumentStatement: string;
	publicationStatement: string;
	issuesPresented: string;
	appellant: Appellant;

	constructor() {
		this.circuitCourtCase = new CircuitCourtCase();
		this.appendexDocuments = "";
		this.conclusion = "";
		this.caseFactsStatement = "";
		this.argument = "";
		this.oralArgumentStatement = "";
		this.publicationStatement = "";
		this.issuesPresented = "";
		this.appellant = new Appellant();
	}
}