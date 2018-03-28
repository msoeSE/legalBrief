import { BriefInfo } from "./BriefInfo";

export class ResponseBriefInfo {
  id: string;
  briefInfo: BriefInfo;
  caseFactsStatement: string;
  oralArgumentStatement: string;
  publicationStatement: string;
  issuesPresented: string;

  constructor() {
    this.briefInfo = new BriefInfo();
    this.caseFactsStatement = "";
    this.oralArgumentStatement = "";
    this.publicationStatement = "";
    this.issuesPresented = "";
	}
}
