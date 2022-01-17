import { Component, OnInit } from '@angular/core';
import { ArticleDescription } from 'src/app/models/article-description.model';
import { Article } from 'src/app/models/article.model';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css']
})
export class ArticleListComponent implements OnInit {

  articleDescriptions?: ArticleDescription[];
  selectedArticle? : ArticleDescription = <ArticleDescription>{};
  currentIndex = -1;
  titleToSearch = '';

  constructor(private articleSerivice: ArticlesService) { }

  ngOnInit(): void {
    this.getArticles();
  }

  getArticles() : void {
    this.articleSerivice.getAll().subscribe(
      {
        next: data => {
          console.log(data);
          this.articleDescriptions = data;
        },
        error: err => console.log(err)
      }
    );
  }

  refreshList() : void {
    this.getArticles();
    this.selectedArticle = <ArticleDescription>{};
    this.currentIndex = -1;
  }

  setActiveArticle(article: ArticleDescription, currentIndex: number) : void {
    this.selectedArticle = article;
    this.currentIndex = currentIndex;
  }

  searchTitle() : void {
    this.currentIndex = -1;
    this.selectedArticle = <ArticleDescription>{};
    this.articleSerivice.findByTitle(this.titleToSearch).subscribe(
      {
        next: data => {
          console.log(data);
          this.articleDescriptions = data;
        },
        error: err => console.log(err)
      }
    );
  }
}
