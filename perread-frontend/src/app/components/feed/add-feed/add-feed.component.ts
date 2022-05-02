import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Feed } from 'src/app/models/feed/feed.model';
import { FeedService } from 'src/app/services/feed.service';

@Component({
  selector: 'app-add-feed',
  templateUrl: './add-feed.component.html',
  styleUrls: ['./add-feed.component.css']
})
export class AddFeedComponent implements OnInit {

  feedName: string = '';
  feedDescription: string = '';

  constructor(private feedService: FeedService, private router: Router) { }

  ngOnInit(): void {

  }

  saveFeed() : void {
    this.feedService.addFeed(this.feedName).subscribe({
      next: data => {
        console.log(data);
        this.router.navigate(['/feed-management'], { replaceUrl: true });
      },
      error: data => console.log(data)
    });
  }
}
