import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateRequestCommand } from 'src/app/models/request/create-request-command.model';
import { RequestPostPublishState, RequestPostPublishStateToLabelMapping } from 'src/app/models/request/request-preview.model';
import { RequestsService } from 'src/app/services/requests.service';

@Component({
  selector: 'app-add-request',
  templateUrl: './add-request.component.html',
  styleUrls: ['./add-request.component.css']
})
export class AddRequestComponent implements OnInit {

  createRequestCommand: CreateRequestCommand = new CreateRequestCommand();
  
  public mapping = RequestPostPublishStateToLabelMapping;
  public statuses = Object.values(RequestPostPublishState);
  selectedStatus = new FormControl();

  constructor(
    private requestService: RequestsService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.createRequestCommand.targetAuthorId = String(this.route.snapshot.paramMap.get('authorId'));
  }

  sendRequest() : void {
    this.createRequestCommand.requestCommand.postPublishState = this.selectedStatus.value;

    this.requestService.createRequest(this.createRequestCommand).subscribe({
      next: data => {
        console.log(data);
        this.router.navigate([`/requests/${data.requestId}`], { replaceUrl: true });
      }
    });
  }
}
