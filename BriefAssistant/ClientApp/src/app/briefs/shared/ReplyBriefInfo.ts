import { BriefInfo } from "../../shared/BriefInfo";
import { BriefType } from "../../shared/BriefType";

export class ReplyBriefInfo {
  id: string;
  briefInfo: BriefInfo;

  constructor() {
    this.briefInfo = new BriefInfo();
    this.briefInfo.type = BriefType.Reply;
  }
}
