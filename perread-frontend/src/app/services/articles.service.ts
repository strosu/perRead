import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { concat, Observable, switchMap } from 'rxjs';
import { Constants } from '../constants';
import { ArticlePreview } from '../models/article/article-preview.model';
import { Article } from '../models/article/article.model';
import { ArticleCommand } from '../models/article/article-command.model';
import { concatMap } from 'rxjs';
import { ArticleOwnership } from '../models/author/article-ownership.model';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  @Output() onArticleServed: EventEmitter<any> = new EventEmitter();
  constructor(private httpClient: HttpClient) { }

  getAll() : Observable<ArticlePreview[]> {
    return this.httpClient.get<ArticlePreview[]>(`${Constants.BACKENDURL}/article`);
  }

  get(id: any) : Observable<Article> {
    
    var articleObservable = this.httpClient.get<Article>(`${Constants.BACKENDURL}/article/${id}`);
    var piped = articleObservable.pipe(
      switchMap((_ => {
        this.onArticleServed.emit(null);
        return articleObservable;
      })
    ));

    // articleObservable.subscribe({
    //   next: _ => this.onArticleServed.emit(null)
    // });

    return articleObservable;
  }

  create(data: ArticleCommand): Observable<Article> {
    return this.httpClient.post<Article>(`${Constants.BACKENDURL}/articles`, data);
  }

  delete(id: any) : Observable<any> {
    return this.httpClient.delete(`${Constants.BACKENDURL}/articles/${id}`);
  }

  findByTitle(title: string) : Observable<ArticlePreview[]> {
    return this.httpClient.get<ArticlePreview[]>(`${Constants.BACKENDURL}/articles?title=${title}`);
  }

  getOwners(id: string) : Observable<ArticleOwnership> {
    return this.httpClient.get<ArticleOwnership>(`${Constants.BACKENDURL}/articles/${id}/owners`)
  }
}
