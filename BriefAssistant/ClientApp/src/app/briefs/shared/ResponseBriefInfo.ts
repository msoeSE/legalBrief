import { BriefInfo } from "./BriefInfo";
import { BriefType } from "./BriefType";

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
    this.briefInfo.type = BriefType.Response;
  }
}
