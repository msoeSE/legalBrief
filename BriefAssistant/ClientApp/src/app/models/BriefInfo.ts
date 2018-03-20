import { ContactInfo } from "./ContactInfo";
import { CircuitCourtCase } from "./CircuitCourtCase";
import { BriefType } from "./BriefType";

export class BriefInfo {
  id: string;
  circuitCourtCase: CircuitCourtCase;
	appellateCourtCaseNumber: string;
	conclusion: string;
	argument: string;
  contactInfo: ContactInfo;
  type: BriefType;

	constructor() {
    this.circuitCourtCase = new CircuitCourtCase();
    this.appellateCourtCaseNumber = "";
		this.conclusion = "";
		this.argument = "";
    this.contactInfo = new ContactInfo();
	  this.type = BriefType.Initial;
	}
}
