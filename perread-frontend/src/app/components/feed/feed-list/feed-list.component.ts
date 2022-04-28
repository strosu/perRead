import { Component, OnInit } from '@angular/core';
import { FeedPreview } from 'src/app/models/feed/feed-preview.model';
import { FeedService } from 'src/app/services/feed.service';

declare var bootstrap: any;

@Component({
  selector: 'app-feed-list',
  templateUrl: './feed-list.component.html',
  styleUrls: ['./feed-list.component.css']
})

// Represents the list of feeds in the home component
export class FeedListComponent implements OnInit {

  feeds: FeedPreview[] = [];
  selectedFeed: number = 0;
  defaultFeed?: FeedPreview;

  constructor(private feedService: FeedService) { }

  ngOnInit(): void {
    this.feedService.getFeedList().subscribe({
      next: data => {
        console.log(data);
        this.feeds = data;
        this.defaultFeed = this.feeds.length > 0 ? this.feeds[0] : null as any;
      },
      error: error => console.log(error)
    })
  }
}
