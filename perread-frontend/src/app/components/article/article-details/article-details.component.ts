import { Component, OnInit } from '@angular/core';
import { MatButtonToggleChange } from '@angular/material/button-toggle';
import { ActivatedRoute } from '@angular/router';
import { ArticleRecommendEnum } from 'src/app/models/article/article-command.model';
import { Article } from 'src/app/models/article/article.model';
import { ArticlesService } from 'src/app/services/articles.service';
import { UriService } from 'src/app/services/uri.service';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css']
})
export class ArticleDetailsComponent implements OnInit {

  article: Article = <Article>{};
  articleImagePath: string = '';
  selectedVal: string[] = [];

  totalReviewCount: number = 0;
  approvalRate: number = 0;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticlesService,
    private uriService: UriService
  ) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.articleService.get(id).subscribe(
      {
        next: data => {
          this.article = data;
          this.articleRefreshed(data);
        },
        error: err => console.log(err)
      }
    );
  }

toggleChange(event: MatButtonToggleChange) {
  let toggle = event.source;
  
  let argument: ArticleRecommendEnum = ArticleRecommendEnum.Clear;
  
  if (toggle) {
      let group = toggle.buttonToggleGroup;
      if (event.value.some((item: any) => item == toggle.value)) {
          group.value = [toggle.value];
          if (toggle.value == 'yes') {
            argument = ArticleRecommendEnum.Yes;
          }
          else {
            argument = ArticleRecommendEnum.Not;
          }
      }
  }

  this.articleService.recommend(this.article.id, argument).subscribe({
    next: data => {
      console.log(data);
      this.articleRefreshed(data);
    },
    error: err => console.log(err)

  });
}

articleRefreshed(data: Article) : void {
  console.log(data);
  this.article = data;
  this.articleImagePath = this.uriService.getStaticFileUri(this.article.articleImageUrl);
  this.totalReviewCount = this.article.recommendsReadingCount + this.article.notRecommendsReadingCount;
  if (this.totalReviewCount > 0) {
    this.approvalRate = this.article.recommendsReadingCount / this.totalReviewCount * 100;
  }

  if (this.article.currentUserRecommends != null) {
    if (this.article.currentUserRecommends) {
      this.selectedVal = ["yes"];
    }
    else {
      this.selectedVal = ["no"];
    }
  }

  this.articleService.onArticleServed.emit(null);
}
}
