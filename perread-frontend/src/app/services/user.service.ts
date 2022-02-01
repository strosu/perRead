import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { UserPreview } from '../models/user/user-preview.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // @Output() onUpdatedUserInformation: EventEmitter<UserPreview> = new EventEmitter();

  constructor(private httpClient: HttpClient) { }

  getCurrentUserPreview() : Observable<UserPreview> {
    return this.httpClient.get<UserPreview>(`${Constants.BACKENDURL}/user/preview`);
  }

  addMoreTokens(): Observable<number> {
    return this.httpClient.post<number>(`${Constants.BACKENDURL}/user/addtokens`, null);
  }
}
