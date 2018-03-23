import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { IBriefList } from "../models/IBriefList";
import { BriefInfo } from "../models/BriefInfo";
import { InitialBriefInfo } from "../models/InitialBriefInfo";
import { ReplyBriefInfo } from "../models/ReplyBriefInfo";

import { Observable } from 'rxjs/Observable';

@Injectable()
export class BriefService {
	private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

	constructor(private readonly http: HttpClient) { }

	getBriefList(): Observable<IBriefList> {
	  return this.http.get<IBriefList>("/api/briefs");
  }

  getBrief(id: string): Observable<BriefInfo> {
    return this.http.get<BriefInfo>(`/api/briefs/${id}`);
  }

  getInitialBrief(id: string): Observable<InitialBriefInfo> {
    return this.http.get<InitialBriefInfo>(`/api/briefs/initials/${id}`);
  }

  getReplyBrief(id: string): Observable<ReplyBriefInfo> {
    return this.http.get<ReplyBriefInfo>(`/api/briefs/replies/${id}`);
  }

  //TODO add getResponseBrief
  //TODO add getPetitionBrief

  createInitial(brief: InitialBriefInfo): Observable<InitialBriefInfo> {
    return this.http.post<InitialBriefInfo>(`/api/briefs/initialcreate`, JSON.stringify(brief), { headers: this.headers });
  }

  createReply(brief: ReplyBriefInfo): Observable<ReplyBriefInfo> {
    return this.http.post<ReplyBriefInfo>(`/api/briefs/replycreate`, JSON.stringify(brief), { headers: this.headers });
  }

  //TODO add createResponse
  //TODO add createPetition

  updateInitial(brief: InitialBriefInfo): Observable<InitialBriefInfo> {
    const url = `/api/briefs/initialupdate/${brief.briefInfo.id}`;
    return this.http.put<InitialBriefInfo>(url, JSON.stringify(brief), { headers: this.headers });
  }

  updateReply(brief: ReplyBriefInfo): Observable<ReplyBriefInfo> {
    const url = `/api/briefs/replyupdate/${brief.briefInfo.id}`;
    return this.http.put<ReplyBriefInfo>(url, JSON.stringify(brief), { headers: this.headers });
  }

  //TODO add updateResponse
  //TODO add updatePetition
}
