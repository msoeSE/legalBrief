import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { IBriefList } from "../models/IBriefList";
import { BriefInfo } from "../models/BriefInfo";
import { InitialBriefInfo } from "../models/InitialBriefInfo";
import { ReplyBriefInfo } from "../models/ReplyBriefInfo";

import { InitialBriefHolder } from "../models/InitialBriefHolder";
import { ReplyBriefHolder } from "../models/ReplyBriefHolder";

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
    return this.http.get<ReplyBriefInfo>(`/api/briefs/replys/${id}`);
  }

  //TODO add getResponseBrief
  //TODO add getPetitionBrief

	create(brief: BriefInfo): Observable<BriefInfo> {
	  return this.http.post<BriefInfo>("/api/briefs", JSON.stringify(brief), { headers: this.headers });
	}

  createInitial(brief: InitialBriefHolder): Observable<InitialBriefHolder> {
    return this.http.post<InitialBriefHolder>(`/api/briefs/initialcreate`, JSON.stringify(brief), { headers: this.headers });
  }

  createReply(brief: ReplyBriefHolder): Observable<ReplyBriefHolder> {
    return this.http.post<ReplyBriefHolder>(`/api/briefs/replycreate`, JSON.stringify(brief), { headers: this.headers });
  }

  //TODO add createResponse
  //TODO add createPetition

	update(brief: BriefInfo) : Observable<BriefInfo> {
		const url = `/api/briefs/${brief.id}`;
    return this.http.put<BriefInfo>(url, JSON.stringify(brief), { headers: this.headers });
  }

  updateInitial(brief: InitialBriefHolder): Observable<InitialBriefHolder> {
    const url = `/api/briefs/initialupdate/${brief.briefInfo.id}`;
    return this.http.put<InitialBriefHolder>(url, JSON.stringify(brief), { headers: this.headers });
  }

  updateReply(brief: ReplyBriefHolder): Observable<ReplyBriefHolder> {
    const url = `/api/briefs/replyupdate/${brief.briefInfo.id}`;
    return this.http.put<ReplyBriefHolder>(url, JSON.stringify(brief), { headers: this.headers });
  }

  //TODO add updateResponse
  //TODO add updatePetition
}
