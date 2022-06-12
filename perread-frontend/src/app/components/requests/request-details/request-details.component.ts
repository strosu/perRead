import { Component, Input, OnInit } from '@angular/core';
import { RequestPreview } from 'src/app/models/request/request-preview.model';

@Component({
  selector: 'app-request-details',
  templateUrl: './request-details.component.html',
  styleUrls: ['./request-details.component.css']
})
export class RequestDetailsComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {
  }

}
