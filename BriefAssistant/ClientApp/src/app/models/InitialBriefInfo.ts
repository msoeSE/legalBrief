import { BriefInfo } from "./BriefInfo";

export class InitialBriefInfo {
  id: string;
  briefInfo: BriefInfo;
  appendixDocuments: string;
  caseFactsStatement: string;
  oralArgumentStatement: string;
  publicationStatement: string;
  issuesPresented: string;

  constructor() {
    this.briefInfo = new BriefInfo();
    this.appendixDocuments = "";
    this.caseFactsStatement = "";
    this.oralArgumentStatement = "";
    this.publicationStatement = "";
    this.issuesPresented = "";
	}
}
