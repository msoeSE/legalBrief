import { Appellant } from "./Appellant";
import { CircuitCourtCase } from "./CircuitCourtCase";

export class BriefInfo {
	id: string;
	circuitCourtCase: CircuitCourtCase;
	appendixDocuments: string;
	conclusion: string;
	caseFactsStatement: string;
	argument: string;
	oralArgumentStatement: string;
	publicationStatement: string;
	issuesPresented: string;
	appellant: Appellant;

	constructor() {
		this.circuitCourtCase = new CircuitCourtCase();
		this.appendixDocuments = "";
		this.conclusion = "";
		this.caseFactsStatement = "";
		this.argument = "";
		this.oralArgumentStatement = "";
		this.publicationStatement = "";
		this.issuesPresented = "";
		this.appellant = new Appellant();
	}
}