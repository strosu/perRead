import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { concat, Observable, switchMap } from 'rxjs';
import { Constants } from '../constants';
import { ArticlePreview } from '../models/article/article-preview.model';
import { Article } from '../models/article/article.model';
import { ArticleCommand } from '../models/article/article-command.model';
import { concatMap } from 'rxjs';
import { OwnerCollection } from '../models/author/article-ownership.model';
import { UpdateOwnershipCommand } from '../models/author/owner-description.model';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  baseUrl = `${Constants.BACKENDURL}/articles`;

  @Output() onArticleServed: EventEmitter<any> = new EventEmitter();
  constructor(private httpClient: HttpClient) { }

  getAll() : Observable<ArticlePreview[]> {
    return this.httpClient.get<ArticlePreview[]>(this.baseUrl);
  }

  get(id: any) : Observable<Article> {
    
    var articleObservable = this.httpClient.get<Article>(`${this.baseUrl}/${id}`);
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
    return this.httpClient.post<Article>(`${this.baseUrl}`, data);
  }

  delete(id: any) : Observable<any> {
    return this.httpClient.delete(`${this.baseUrl}/${id}`);
  }

  findByTitle(title: string) : Observable<ArticlePreview[]> {
    return this.httpClient.get<ArticlePreview[]>(`${this.baseUrl}?title=${title}`);
  }

  getOwners(id: string) : Observable<OwnerCollection> {
    return this.httpClient.get<OwnerCollection>(`${this.baseUrl}/${id}/owners`)
  }

  updateOwners(id: string, updateOwnershipCommand: UpdateOwnershipCommand) : Observable<OwnerCollection> {
    return this.httpClient.post<OwnerCollection>(`${this.baseUrl}/${id}/owners`, updateOwnershipCommand);
  }
}
