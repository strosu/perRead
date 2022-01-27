import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ArticlePreview } from 'src/app/models/article-preview.model';
import { Constants} from 'src/app/constants'


@Component({
  selector: 'app-article-preview',
  templateUrl: './article-preview.component.html',
  styleUrls: ['./article-preview.component.css']
})
export class ArticlePreviewComponent implements OnInit {

  @Input()
  articlePreview: ArticlePreview = <ArticlePreview>{};

  articleImagePath: string = '';

  createdAt: string = '';

  constructor() { }

  ngOnInit(): void {
    this.createdAt = formatDate(this.articlePreview.articleCreatedAt,'yyyy-MM-dd', 'en');
    this.articleImagePath = `${Constants.BACKENDURL}/${this.articlePreview.articleImageUrl}`;
  }
}
