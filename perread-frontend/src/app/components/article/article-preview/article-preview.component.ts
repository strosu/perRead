import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ArticlePreview } from 'src/app/models/article-preview.model';

@Component({
  selector: 'app-article-preview',
  templateUrl: './article-preview.component.html',
  styleUrls: ['./article-preview.component.css']
})
export class ArticlePreviewComponent implements OnInit {

  @Input()
  articlePreview: ArticlePreview = <ArticlePreview>{};

  createdAt: string = '';

  constructor() { }

  ngOnInit(): void {
    this.createdAt = formatDate(this.articlePreview.articleCreatedAt,'yyyy-MM-dd', 'en');
  }
}
