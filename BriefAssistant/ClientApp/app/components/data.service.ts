import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class DataService {
  private idSource= new Subject<string>();
  setID(id: string)
  {
      this.idSource.next(id);
  }
  getID(): Observable<string>
  {
      return this.idSource.asObservable();
  }
}