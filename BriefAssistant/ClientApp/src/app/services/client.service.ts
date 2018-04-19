import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { IContactList } from '../models/IContactList';
import { ContactInfo } from '../models/ContactInfo';

@Injectable()
export class ClientService {
	private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

	constructor(private readonly http: HttpClient) { }

	getContactList(): Observable<IContactList> {
	  return this.http.get<IContactList>("/api/clients");
  }

  createContact(client: ContactInfo): Observable<ContactInfo> {
    return this.http.post<ContactInfo>(`/api/clients/createClient`, JSON.stringify(client), { headers: this.headers });
  }
}
