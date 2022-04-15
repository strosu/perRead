import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FeedPreview } from '../models/feed/feed-preview.model';
import { Constants } from '../constants';
import { Feed } from '../models/feed/feed.model';
import { FeedInfo } from '../models/feed/feed-info.model';

@Injectable({
  providedIn: 'root'
})

export class FeedService {

  constructor(private httpClient: HttpClient) { }

  getFeedList() : Observable<FeedPreview[]>{
    return this.httpClient.get<FeedPreview[]>(`${Constants.BACKENDURL}/feeds`);
  }

  getFeedDetails(feedId: string) : Observable<Feed>{
    return this.httpClient.get<Feed>(`${Constants.BACKENDURL}/feed/${feedId}`);
  } 

  addAuthorToFeed(feedId: string, authorId: string) : Observable<any> {
    return this.httpClient.post(`${Constants.BACKENDURL}/feed/${feedId}/add/${authorId}`, null);
  }

  getFeedInfo(feedId: string) : Observable<FeedInfo> {
    return this.httpClient.get<FeedInfo>(`${Constants.BACKENDURL}/feed/info/${feedId}`)
  }
}
