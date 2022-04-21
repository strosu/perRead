import { Component, OnInit } from '@angular/core';
import { FeedPreview } from 'src/app/models/feed/feed-preview.model';
import { FeedService } from 'src/app/services/feed.service';

@Component({
  selector: 'app-feed-management',
  templateUrl: './feed-management.component.html',
  styleUrls: ['./feed-management.component.css']
})
export class FeedManagementComponent implements OnInit {

  // TODO - this should be a generic class, shared with FeedListComponent. The only difference is the type of inner component here
  feeds: FeedPreview[] = [];
  selectedFeed: number = 0;
  
  constructor(private feedService: FeedService) { }

  ngOnInit(): void {
    this.feedService.getFeedList().subscribe({
      next: data => {
        console.log(data);
        this.feeds = data;
        // this.defaultFeed = this.feeds.length > 0 ? this.feeds[0] : null as any;
      },
      error: error => console.log(error)
    })
  }

}
