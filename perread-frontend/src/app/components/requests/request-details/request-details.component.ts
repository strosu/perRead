import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RequestPostPublishState, RequestPostPublishStateToLabelMapping, RequestPreview } from 'src/app/models/request/request-preview.model';
import { ArticleRequest } from 'src/app/models/request/article-request.model';
import { RequestsService } from 'src/app/services/requests.service';

@Component({
  selector: 'app-request-details',
  templateUrl: './request-details.component.html',
  styleUrls: ['./request-details.component.css']
})
export class RequestDetailsComponent implements OnInit {
  
  request: ArticleRequest = <ArticleRequest>{};
  public mapping = RequestPostPublishStateToLabelMapping;

  constructor(
    private route: ActivatedRoute,
    private requestService: RequestsService
  ) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.requestService.getRequest(id).subscribe({
      next: data => {
        console.log(data);
        this.request = data;
      },
      error: err => console.log(err)
    });
  }

  deleteRequest() : void {
  }

}
