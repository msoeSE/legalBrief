import { ContactInfo } from "./ContactInfo";
import { CircuitCourtCase } from "./CircuitCourtCase";

export class BriefInfo {
	id: string;circuitCourtCase: CircuitCourtCase;
	appellateCourtCaseNumber: string;
	appendixDocuments: string;
	conclusion: string;
	caseFactsStatement: string;
	argument: string;
	oralArgumentStatement: string;
	publicationStatement: string;
	issuesPresented: string;
  contactInfo: ContactInfo;
  title: string;

	constructor() {
    this.circuitCourtCase = new CircuitCourtCase();
    this.appellateCourtCaseNumber = "";
		this.appendixDocuments = "";
		this.conclusion = "";
		this.caseFactsStatement = "";
		this.argument = "";
		this.oralArgumentStatement = "";
		this.publicationStatement = "";
		this.issuesPresented = "";
    this.contactInfo = new ContactInfo();
	  this.title = "";
	}
}
