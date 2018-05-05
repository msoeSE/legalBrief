import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { RegistrationRequest } from './RegistrationRequest';
import { ResetPasswordRequest } from './ResetPasswordRequest';
import { EmailRequest } from '../../shared/EmailRequest';

@Injectable()
export class AccountService {
  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private readonly http: HttpClient) {}

  register(model: RegistrationRequest): Observable<object> {
    return this.http.post("/api/account/register", JSON.stringify(model), { headers: this.headers });
  }

  confirmEmail(userId: string, code: string) : Observable<object> {
    return this.http.get(`/api/account/confirmEmail?userId=${userId}&code=${code}`);
  }

  forgotPassword(model: EmailRequest): Observable<object> {
    return this.http.post("/api/account/forgotPassword", JSON.stringify(model), { headers: this.headers });
  }

  resetPassword(model: ResetPasswordRequest): Observable<object> {
    return this.http.post("/api/account/resetPassword", JSON.stringify(model), { headers: this.headers });
  }
}
