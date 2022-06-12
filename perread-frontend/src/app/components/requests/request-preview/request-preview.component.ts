import { Component, Input, OnInit } from '@angular/core';
import { RequestPreview } from 'src/app/models/request/request-preview.model';

@Component({
  selector: 'app-request-preview',
  templateUrl: './request-preview.component.html',
  styleUrls: ['./request-preview.component.css']
})
export class RequestPreviewComponent implements OnInit {

  @Input()
  requestPreview: RequestPreview = <RequestPreview>{};

  constructor() { }

  ngOnInit(): void {
  }

}
