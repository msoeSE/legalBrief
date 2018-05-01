import { BriefInfo } from "../../shared/BriefInfo";
import { BriefType } from "../../shared/BriefType";

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
    this.briefInfo.type = BriefType.Initial;
    this.appendixDocuments = "";
    this.caseFactsStatement = "";
    this.oralArgumentStatement = "";
    this.publicationStatement = "";
    this.issuesPresented = "";
  }
}
