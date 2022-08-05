import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RequestPostPublishState, RequestPostPublishStateToLabelMapping, RequestPreview, RequestState } from 'src/app/models/request/request-preview.model';
import { ArticleRequest } from 'src/app/models/request/article-request.model';
import { RequestsService } from 'src/app/services/requests.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-request-details',
  templateUrl: './request-details.component.html',
  styleUrls: ['./request-details.component.css']
})
export class RequestDetailsComponent implements OnInit {
  
  request: ArticleRequest = <ArticleRequest>{};
  
  canBeAccepted: boolean = false;
  canBeCompleted: boolean = false;
  canBeAbandoned: boolean = false;
  public mapping = RequestPostPublishStateToLabelMapping;

  constructor(
    private route: ActivatedRoute,
    private requestService: RequestsService,
    private tokenStorageService: TokenStorageService
  ) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.requestService.getRequest(id).subscribe({
      next: data => {
        console.log(data);
        this.request = data;
        this.computeEditingModes();
      },
      error: err => console.log(err)
    });
  }

  acceptRequest(): void {
    this.requestService.acceptRequest(this.request.requestId).subscribe(
      {
        next: data => {
          console.log(data);
          this.request = data;
          this.computeEditingModes();
        },
        error: err => console.log(err)
      }
    );
  }

  computeEditingModes() : void {
    if (this.request.targetAuthor.authorName != this.tokenStorageService.getUser().userName) {
      return;
    }

    if (this.request.requestState === RequestState.Created) {
      this.canBeAccepted = true;
      return;
    }

    if (this.request.requestState === RequestState.Accepted) {
      this.canBeCompleted = true;
    }
  }
}
