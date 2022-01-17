import { Injectable } from '@angular/core';
import { Article } from './article';
import { MOCKARTICLES } from './mock-articles';
import { lastValueFrom, Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class ArticleService {

  constructor(
    private messageService: MessageService,
    private httpClient: HttpClient) { }

  async getArticles(): Promise<Article[]> {
    // const articles = of(MOCKARTICLES);

    const articles = this.httpClient.get<Article[]>('https://localhost:7176/Article');

    let result = await lastValueFrom(articles);

    this.messageService.add('Fetched the articles from the server');

    return result;
  }

  async getArticle(id: number): Promise<Article> {

    let articleObservable = this.httpClient.get<Article>(`https://localhost:7176/article/${id}`);
    
    const article = await lastValueFrom(articleObservable);
    this.messageService.add(`Loaded article ${article?.articleId}`);

    return article;
  }
}
