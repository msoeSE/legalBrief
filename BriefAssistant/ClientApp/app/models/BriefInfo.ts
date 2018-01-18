import { ContactInfo } from "./ContactInfo";
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
	contactInfo: ContactInfo;

	constructor() {
		this.circuitCourtCase = new CircuitCourtCase();
		this.appendixDocuments = "";
		this.conclusion = "";
		this.caseFactsStatement = "";
		this.argument = "";
		this.oralArgumentStatement = "";
		this.publicationStatement = "";
		this.issuesPresented = "";
		this.contactInfo = new ContactInfo();
	}
}