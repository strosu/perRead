import { Component, OnInit } from '@angular/core';
import { Article } from '../article';
import { MOCKARTICLES } from '../mock-articles';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})

export class ArticlesComponent implements OnInit {

  articles = MOCKARTICLES;
  selectedArticle?: Article;

  constructor() { }

  ngOnInit(): void {
  }

  onSelect(article: Article) {
    this.selectedArticle = article;
  }

}
