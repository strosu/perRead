import { Component, Input, OnInit } from '@angular/core';
import { FeedDetails } from 'src/app/models/feed/feed-details.model';
import { FeedPreview } from 'src/app/models/feed/feed-preview.model';
import { FeedService } from 'src/app/services/feed.service';

@Component({
  selector: 'app-feed-details',
  templateUrl: './feed-details.component.html',
  styleUrls: ['./feed-details.component.css']
})
export class FeedDetailsComponent implements OnInit {

  @Input()
  feedPreview: FeedPreview = <FeedPreview>{};
  
  feedDetails: FeedDetails = <FeedDetails>{};

  constructor(private feedService: FeedService) { }

  ngOnInit(): void {
    this.feedService.getFeedDetails(this.feedPreview.feedId).subscribe(
      {
        next: data => {
          console.log(data);
          this.feedDetails = data;
        },
        error : err => console.log(err)
      }
    );
  }

}
