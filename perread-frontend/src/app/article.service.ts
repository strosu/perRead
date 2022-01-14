import { Injectable } from '@angular/core';
import { Article } from './article';
import { MOCKARTICLES } from './mock-articles';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class ArticleService {

  constructor(
    private messageService: MessageService,
    private httpClient: HttpClient) { }

  getArticles(): Observable<Article[]> {
    // const articles = of(MOCKARTICLES);

    const articles = this.httpClient.get<Article[]>('https://localhost:7176/Article');

    this.messageService.add('Fetched the articles from the server');
    return articles;
  }

  getArticle(id: number): Observable<Article> {
    const article = MOCKARTICLES.find(x => x.articleId === id)!;
    this.messageService.add(`Loaded article ${article?.articleId}`);

    return of(article);
  }
}
