import { Component, Input, OnInit } from '@angular/core';
import { RequestPreview } from 'src/app/models/request/request-preview.model';
import { RequestsService } from 'src/app/services/requests.service';

@Component({
  selector: 'app-author-requests',
  templateUrl: './author-requests.component.html',
  styleUrls: ['./author-requests.component.css']
})
export class AuthorRequestsComponent implements OnInit {

  @Input()
  authorId: string = '';

  requestList: RequestPreview[] = [];

  constructor(private requestService: RequestsService) { }

  ngOnInit(): void {
    this.requestService.listRequests(this.authorId).subscribe({
      next: data => {
        console.log(data);
        this.requestList = data;
      },
      error: err => console.log(err)
    });
  }

}
