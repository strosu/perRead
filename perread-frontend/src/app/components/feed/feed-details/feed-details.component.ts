import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FeedDetails } from 'src/app/models/feed/feed-details.model';
import { FeedService } from 'src/app/services/feed.service';

@Component({
  selector: 'app-feed-details',
  templateUrl: './feed-details.component.html',
  styleUrls: ['./feed-details.component.css']
})
export class FeedDetailsComponent implements OnInit {
  
  feedDetails: FeedDetails = <FeedDetails>{};

  constructor(
    private feedService: FeedService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));

    this.feedService.getFeedDetails(id).subscribe(
      {
        next: data => {
          console.log(data);
          this.feedDetails = data;
        },
        error : err => console.log(err)
      }
    );
  }

  updateFeed() : void {
    this.feedService.updateFeed(this.feedDetails.feedId, this.feedDetails).subscribe({
      next: data => {
        console.log(data);
        this.router.navigate(['/feed-management'], { replaceUrl: true });
      },
      error: err => console.log(err)
    });
  }

  deleteFeed() : void {
    this.feedService.deleteFeed(this.feedDetails.feedId).subscribe({
      next: data => {
        console.log(data);
        this.router.navigate(['/feed-management'], { replaceUrl: true });
      },
      error: err => console.log(err)
    });
  }

}
