import { Component, Input, OnInit } from '@angular/core';
import { ArticlePreview } from 'src/app/models/article/article-preview.model';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.css']
})
export class ArticleListComponent implements OnInit {

  @Input()
  articleDescriptions: ArticlePreview[] = [];
  
  selectedArticle : ArticlePreview = <ArticlePreview>{};
  currentIndex = -1;
  // titleToSearch = '';

  constructor() { }

  ngOnInit(): void {
    // this.getArticles();
  }

  // getArticles() : void {
  //   this.articleSerivice.getAll().subscribe(
  //     {
  //       next: data => {
  //         console.log(data);
  //         this.articleDescriptions = data;
  //       },
  //       error: err => console.log(err)
  //     }
  //   );
  // }

  setActiveArticle(article: ArticlePreview, currentIndex: number) : void {
    this.selectedArticle = article;
    this.currentIndex = currentIndex;
  }

  // searchTitle() : void {
  //   this.currentIndex = -1;
  //   this.selectedArticle = <ArticlePreview>{};
  //   this.articleSerivice.findByTitle(this.titleToSearch).subscribe(
  //     {
  //       next: data => {
  //         console.log(data);
  //         this.articleDescriptions = data;
  //       },
  //       error: err => console.log(err)
  //     }
  //   );
  // }
}
