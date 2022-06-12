import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { Tag } from '../models/tag/tag.model';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  constructor(private httpclient: HttpClient) { }

  getTag(id: number): Observable<Tag> {
    return this.httpclient.get<Tag>(`${Constants.BACKENDURL}/tags/${id}`)
  }
}
