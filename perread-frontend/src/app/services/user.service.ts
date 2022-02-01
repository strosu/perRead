import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { UserPreview } from '../models/user/user-preview.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  getCurrentUserPreview() : Observable<UserPreview> {
    return this.httpClient.get<UserPreview>(`${Constants.BACKENDURL}/user/preview`);
  }
}
