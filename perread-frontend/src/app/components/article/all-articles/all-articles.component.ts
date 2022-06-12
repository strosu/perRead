import { Component, OnInit } from '@angular/core';
import { ArticlePreview } from 'src/app/models/article/article-preview.model';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-all-articles',
  templateUrl: './all-articles.component.html',
  styleUrls: ['./all-articles.component.css']
})
export class AllArticlesComponent implements OnInit {

  articlePreviews: ArticlePreview[] = [];

  constructor(private articleSerivice: ArticlesService) { }

  ngOnInit(): void {
    this.getArticles();
  }

  getArticles() : void {
    this.articleSerivice.getAll().subscribe(
      {
        next: data => {
          console.log(data);
          this.articlePreviews = data;
        },
        error: err => console.log(err)
      }
    );
  }
}