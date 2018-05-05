import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { IBriefList } from "./IBriefList";
import { BriefInfo } from "./BriefInfo";
import { InitialBriefInfo } from "./InitialBriefInfo";
import { ReplyBriefInfo } from "./ReplyBriefInfo";
import { ResponseBriefInfo } from "./ResponseBriefInfo";
import { EmailRequest } from '../../shared/EmailRequest';

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

  getResponseBrief(id: string): Observable<ResponseBriefInfo> {
    return this.http.get<ResponseBriefInfo>(`/api/briefs/responses/${id}`);
  }

  createInitial(brief: InitialBriefInfo): Observable<InitialBriefInfo> {
    return this.http.post<InitialBriefInfo>('/api/briefs/initialcreate', JSON.stringify(brief), { headers: this.headers });
  }

  createReply(brief: ReplyBriefInfo): Observable<ReplyBriefInfo> {
    return this.http.post<ReplyBriefInfo>(`/api/briefs/replycreate`, JSON.stringify(brief), { headers: this.headers });
  }

  createResponse(brief: ResponseBriefInfo): Observable<ResponseBriefInfo> {
    return this.http.post<ResponseBriefInfo>(`/api/briefs/responsecreate`, JSON.stringify(brief), { headers: this.headers });
  }

  updateInitial(brief: InitialBriefInfo): Observable<InitialBriefInfo> {
    const url = `/api/briefs/initialupdate/${brief.briefInfo.id}`;
    return this.http.put<InitialBriefInfo>(url, JSON.stringify(brief), { headers: this.headers });
  }

  updateReply(brief: ReplyBriefInfo): Observable<ReplyBriefInfo> {
    const url = `/api/briefs/replyupdate/${brief.briefInfo.id}`;
    return this.http.put<ReplyBriefInfo>(url, JSON.stringify(brief), { headers: this.headers });
  }

  updateResponse(brief: ResponseBriefInfo): Observable<ResponseBriefInfo> {
    const url = `/api/briefs/responseupdate/${brief.briefInfo.id}`;
    return this.http.put<ResponseBriefInfo>(url, JSON.stringify(brief), { headers: this.headers });
  }

  downloadBrief(id: string): Observable<File> {
    return this.http.get(`/api/briefs/${id}/download`, { responseType: 'blob', observe: 'response'})
      .map(res => {
        var mimeType = res.headers.get('Content-Type');
        var blob = new Blob([res.body], { type: mimeType });
        var filenameKvp = res.headers.get('Content-Disposition').split(';')[1].trim();
        var filename = filenameKvp.substr(filenameKvp.indexOf('=') + 1);
        return new File([blob], filename);
      });
  }

  emailBrief(id: string, request: EmailRequest): Observable<object> {
    return this.http.post(`/api/briefs/${id}/email`, JSON.stringify(request), {headers: this.headers});
  }
}
