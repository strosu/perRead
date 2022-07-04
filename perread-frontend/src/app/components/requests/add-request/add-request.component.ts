import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PledgeCommand } from 'src/app/models/pledge/pledge--command.model';
import { CreateRequestCommand } from 'src/app/models/request/create-request-command.model';
import { RequestCommand } from 'src/app/models/request/request-command.model';
import { RequestPostPublishState } from 'src/app/models/request/request-preview.model';
import { RequestsService } from 'src/app/services/requests.service';

@Component({
  selector: 'app-add-request',
  templateUrl: './add-request.component.html',
  styleUrls: ['./add-request.component.css']
})
export class AddRequestComponent implements OnInit {

  requestCommand: RequestCommand = <RequestCommand>{};
  pledgeCommand: PledgeCommand = <PledgeCommand>{};

  statuses: RequestPostPublishState[] = [ RequestPostPublishState.Exclusive, RequestPostPublishState.ProfitShare, RequestPostPublishState.Public ];
  selectedStatus = new FormControl();

  constructor(
    private requestService: RequestsService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.requestCommand.targetAuthorId = String(this.route.snapshot.paramMap.get('authorId'));
  }

  sendRequest() : void {
    let createRequestCommand : CreateRequestCommand = {
      requestCommand: this.requestCommand,
      pledgeCommand : this.pledgeCommand
    }

    this.requestService.createRequest(createRequestCommand).subscribe({
      next: data => {
        console.log(data);
        this.router.navigate([`/requests/${data.requestId}`], { replaceUrl: true });
      }
    });
  }
}
