import { BriefInfo } from "./BriefInfo";
import { BriefType } from "./BriefType";

export class ReplyBriefInfo {
  id: string;
  briefInfo: BriefInfo;

  constructor() {
    this.briefInfo = new BriefInfo();
    this.briefInfo.type = BriefType.Reply;
  }
}
