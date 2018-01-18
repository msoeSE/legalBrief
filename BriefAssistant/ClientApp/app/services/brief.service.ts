import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { IBriefList } from "../models/IBriefList";
import { BriefInfo } from "../models/BriefInfo";

import 'rxjs/add/operator/toPromise';

@Injectable()
export class BriefService {
	private headers = new Headers({ 'Content-Type': 'application/json' });

	constructor(private readonly http: Http) { }

	getBriefList(): Promise<IBriefList> {
		return this.http.get("/api/briefs/")
			.toPromise()
			.then(response => response.json().data as IBriefList);
	}

	getBrief(id: string): Promise<BriefInfo> {
		return this.http.get(`/api/briefs/${id}`)
			.toPromise()
			.then(response => response.json().data as BriefInfo);
	}

	create(brief: BriefInfo): Promise<BriefInfo> {
		return this.http.post("/api/briefs/", JSON.stringify(brief), {headers: this.headers})
			.toPromise()
			.then(response => response.json().data as BriefInfo);
	}

	update(brief: BriefInfo) {
		const url = `/api/briefs/${brief.id}`;
		return this.http.put(url, JSON.stringify(brief), { headers: this.headers })
			.toPromise()
			.then(() => brief);
	}
}