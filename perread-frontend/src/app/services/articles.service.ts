import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { ArticlePreview } from '../models/article/article-preview.model';
import { Article } from '../models/article/article.model';
import { ArticleCommand } from '../models/article/article-command.model';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  constructor(private httpClient: HttpClient) { }

  getAll() : Observable<ArticlePreview[]> {
    return this.httpClient.get<ArticlePreview[]>(`${Constants.BACKENDURL}/article`);
  }

  get(id: any) : Observable<Article> {
    return this.httpClient.get<Article>(`${Constants.BACKENDURL}/article/${id}`);
  }

  create(data: ArticleCommand): Observable<Article> {
    return this.httpClient.post<Article>(`${Constants.BACKENDURL}/article`, data);
  }

  delete(id: any) : Observable<any> {
    return this.httpClient.delete(`${Constants.BACKENDURL}/article/${id}`);
  }

  findByTitle(title: string) : Observable<ArticlePreview[]> {
    return this.httpClient.get<ArticlePreview[]>(`${Constants.BACKENDURL}/article?title=${title}`);
  }
}
