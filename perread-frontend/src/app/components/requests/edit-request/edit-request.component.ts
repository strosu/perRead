import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
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
  
  constructor(private requestService: RequestsService) { }

  ngOnInit(): void {
  }

  saveRequest() : void {

  }

}
