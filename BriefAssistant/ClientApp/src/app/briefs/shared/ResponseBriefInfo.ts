import { BriefInfo } from "../../shared/BriefInfo";
import { BriefType } from "../../shared/BriefType";

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
