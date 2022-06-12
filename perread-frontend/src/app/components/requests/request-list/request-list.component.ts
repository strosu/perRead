import { Component, Input, OnInit } from '@angular/core';
import { RequestPreview } from 'src/app/models/request/request-preview.model';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.css']
})
export class RequestListComponent implements OnInit {

  @Input()
  requestPreviews: RequestPreview[] = [];
  
  constructor() { }

  ngOnInit(): void {
  }

}
