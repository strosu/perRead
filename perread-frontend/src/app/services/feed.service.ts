import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FeedPreview } from '../models/feed/feed-preview.model';
import { Constants } from '../constants';
import { Feed } from '../models/feed/feed.model';
import { FeedDetails } from '../models/feed/feed-details.model';

@Injectable({
  providedIn: 'root'
})

export class FeedService {

  constructor(private httpClient: HttpClient) { }

  getFeedList() : Observable<FeedPreview[]>{
    return this.httpClient.get<FeedPreview[]>(`${Constants.BACKENDURL}/feeds`);
  }

  getFeedWithArticles(feedId: string) : Observable<Feed>{
    return this.httpClient.get<Feed>(`${Constants.BACKENDURL}/feeds/${feedId}`);
  } 

  addFeed(feedName: string) : Observable<Feed> {
    return this.httpClient.post<Feed>(`${Constants.BACKENDURL}/feeds/add/${feedName}`, null);
  }

  addSectionToFeed(feedId: string, sectionId: string) : Observable<any> {
    return this.httpClient.post(`${Constants.BACKENDURL}/feeds/${feedId}/add/${sectionId}`, null);
  }

  getFeedDetails(feedId: string) : Observable<FeedDetails> {
    return this.httpClient.get<FeedDetails>(`${Constants.BACKENDURL}/feeds/${feedId}/details`)
  }

  updateFeed(feedId: string, feedDetails: FeedDetails) : Observable<any>{
    return this.httpClient.post(`${Constants.BACKENDURL}/feeds/${feedId}/details`, feedDetails);
  }

  deleteFeed(feedId: string) : Observable<any> {
    return this.httpClient.delete(`${Constants.BACKENDURL}/feeds/${feedId}`);
  }
}
