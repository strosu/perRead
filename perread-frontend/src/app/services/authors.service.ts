import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { AuthorPreview } from '../models/author-preview.model';
import { Author } from '../models/author.model';

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {

  constructor(private httpClient: HttpClient) { }

  getAll() : Observable<AuthorPreview[]> {
    return this.httpClient.get<AuthorPreview[]>(`${Constants.BACKENDURL}/authors`);
  }

  getAuthor(id: string) : Observable<Author> {
    return this.httpClient.get<Author>(`${Constants.BACKENDURL}/authors/${id}`);
  }

  getCurrentAuthor() : Observable<AuthorPreview> {
    return this.httpClient.get<AuthorPreview>(`${Constants.BACKENDURL}/authors/details`);
  }
}
