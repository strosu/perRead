import { Component, Input, OnInit } from '@angular/core';
import { ArticlePreview } from 'src/app/models/article/article-preview.model';
import { FeedPreview } from 'src/app/models/feed/feed-preview.model';
import { Feed } from 'src/app/models/feed/feed.model';
import { FeedService } from 'src/app/services/feed.service';

@Component({
  selector: 'app-feed-articles',
  templateUrl: './feed-articles.component.html',
  styleUrls: ['./feed-articles.component.css']
})
export class FeedArticlesComponent implements OnInit {

  @Input()
  feedPreview: FeedPreview = <FeedPreview>{};
  hasAnyArticles: boolean = false;

  articles : ArticlePreview[] = [];

  constructor(private feedService: FeedService) { }

  ngOnInit(): void {
    this.feedService.getFeedWithArticles(<string>this.feedPreview.feedId).subscribe(
      {
        next : data => {
          console.log(data);
          this.articles = data.articlePreviews;
          this.hasAnyArticles = this.articles?.length != null && this.articles.length > 0;
        },
        error: error => console.log(error)
      }
    );
  }
}
