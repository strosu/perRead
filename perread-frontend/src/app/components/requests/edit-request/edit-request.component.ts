import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestCommand } from 'src/app/models/request/request-command.model';
import { RequestPostPublishState } from 'src/app/models/request/request-preview.model';
import { RequestsService } from 'src/app/services/requests.service';

@Component({
  selector: 'app-edit-request',
  templateUrl: './edit-request.component.html',
  styleUrls: ['./edit-request.component.css']
})
export class EditRequestComponent implements OnInit {

  requestCommand: RequestCommand = <RequestCommand>{};
  statuses: RequestPostPublishState[] = [ RequestPostPublishState.Exclusive, RequestPostPublishState.ProfitShare, RequestPostPublishState.Public ];
  selectedStatus = new FormControl();
  
  constructor(private requestService: RequestsService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.requestCommand.requestId = id;
    this.requestService.getRequest(id).subscribe({
      next: data => {
        console.log(data);
        this.requestCommand.title = data.title;
        this.requestCommand.deadline = data.deadLine;
        this.requestCommand.description = data.description;
        this.requestCommand.percentForPledgers = data.percentForledgers;
        this.requestCommand.postPublishState = data.postPublishState;
        // Probably don't allow targetting another authoer when editing a request
      },
      error: err => console.log(err)
    });
  }

  saveRequest() : void {
    this.requestService.editRequest(this.requestCommand).subscribe({
      next: data => {
        console.log(data);
        this.router.navigate([`/requests/${this.requestCommand.requestId}`], { replaceUrl: true });
      }
    });
  }

}
