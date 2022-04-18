import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { ArticleUnlockInfo } from '../models/user/article-unlock-info';
import { UserPreview } from '../models/user/user-preview.model';
import { UserSettings } from '../models/user/user-settings.model';

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

  getCurrentUserSettings(): Observable<UserSettings> {
    return this.httpClient.get<UserSettings>(`${Constants.BACKENDURL}/user/settings`);
  }

  updateCurrentUserSettings(data: UserSettings) : Observable<UserSettings> {
    return this.httpClient.post<UserSettings>(`${Constants.BACKENDURL}/user/settings`, data);
  }

  getCurrentUserUnlockedArticles() : Observable<ArticleUnlockInfo[]> {
    return this.httpClient.get<ArticleUnlockInfo[]>(`${Constants.BACKENDURL}/user/acquired`);
  }

  updateCurrentUserUnlockedArticles(articles: number[]) : Observable<any> {
    return this.httpClient.post<number[]>(`${Constants.BACKENDURL}/user/acquired`, articles);
  }
}
