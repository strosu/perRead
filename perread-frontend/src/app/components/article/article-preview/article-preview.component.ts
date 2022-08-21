import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { UrlHelper } from 'src/app/helpers/url-helper.model';
import { ArticlePreview, ReadingState } from 'src/app/models/article/article-preview.model';
import { UriService } from 'src/app/services/uri.service';

@Component({
  selector: 'app-article-preview',
  templateUrl: './article-preview.component.html',
  styleUrls: ['./article-preview.component.css']
})
export class ArticlePreviewComponent implements OnInit {

  @Input()
  articlePreview: ArticlePreview = <ArticlePreview>{};

  articleUrl: string = '';
  articleImagePath: string = '';

  createdAt: string = '';
  articleState: string = '';

  constructor(private uriService: UriService) { }

  ngOnInit(): void {
    this.articleState = this.articlePreview.readingState;
    this.createdAt = formatDate(this.articlePreview.articleCreatedAt,'yyyy-MM-dd', 'en');
    this.articleImagePath = this.uriService.getStaticFileUri(this.articlePreview.articleImageUrl);
    this.articleUrl = UrlHelper.getArticleUrl(this.articlePreview.articleId);
  }
}
