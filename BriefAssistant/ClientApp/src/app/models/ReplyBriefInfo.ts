import { BriefInfo } from "./BriefInfo";

export class ReplyBriefInfo {
  id: string;
  briefInfo: BriefInfo;

  constructor() {
    this.briefInfo = new BriefInfo();
  }
}
