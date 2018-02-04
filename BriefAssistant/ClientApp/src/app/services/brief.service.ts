import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { IBriefList } from "../models/IBriefList";
import { BriefInfo } from "../models/BriefInfo";

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

	create(brief: BriefInfo): Observable<BriefInfo> {
	  return this.http.post<BriefInfo>("/api/briefs", JSON.stringify(brief), { headers: this.headers });
	}

	update(brief: BriefInfo) : Observable<any> {
		const url = `/api/briefs/${brief.id}`;
    return this.http.put(url, JSON.stringify(brief), { headers: this.headers });
	}
}
