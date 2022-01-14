import { Component, OnInit } from '@angular/core';
import { Article } from '../article';
import { ArticleService } from '../article.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})

export class ArticlesComponent implements OnInit {

  articles: Article[] = [];
  selectedArticle?: Article;

  constructor(
    private articleService: ArticleService, 
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadArticles();
  }

  loadArticles(): void {
     this.articleService.getArticles()
     .subscribe(articles => this.articles = articles);
  }

  onSelect(article: Article) {
    this.selectedArticle = article;
    this.messageService.add(`Selected a new article with id ${article.articleId}`)
  }

}
