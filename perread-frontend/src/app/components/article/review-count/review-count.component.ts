import { Component, Input, OnInit } from '@angular/core';
import { ArticlePreview } from 'src/app/models/article/article-preview.model';
import { Article } from 'src/app/models/article/article.model';

@Component({
  selector: 'app-review-count',
  templateUrl: './review-count.component.html',
  styleUrls: ['./review-count.component.css']
})
export class ReviewCountComponent implements OnInit {

  @Input()
  article: ArticlePreview | Article = <Article>{};

  approvalRate: number = 0;
  totalReviewCount: number = 0;

  constructor() { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: any): void {
    this.totalReviewCount = this.article.recommendsReadingCount + this.article.notRecommendsReadingCount;
    if (this.totalReviewCount > 0) {
      this.approvalRate = this.article.recommendsReadingCount / this.totalReviewCount * 100;
    }
  }

}
