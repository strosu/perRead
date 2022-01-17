import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Article } from '../models/article.model';

const baseUrl = 'https://localhost:7176';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  constructor(private httpClient: HttpClient) { }

  getAll() : Observable<Article[]> {
    return this.httpClient.get<Article[]>(`${baseUrl}/article`);
  }

  get(id: any) : Observable<Article> {
    return this.httpClient.get<Article>(`${baseUrl}/article/${id}`);
  }

  create(data: any): Observable<Article> {
    return this.httpClient.post(`${baseUrl}/article`, data);
  }

  delete(id: any) : Observable<any> {
    return this.httpClient.delete(`${baseUrl}/article/${id}`);
  }

  findByTitle(title: any) : Observable<Article[]> {
    return this.httpClient.get<Article[]>(`${baseUrl}/article?title=${title}`);
  }
}
