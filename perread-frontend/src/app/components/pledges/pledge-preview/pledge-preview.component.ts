import { Component, Input, OnInit } from '@angular/core';
import { PledgePreview } from 'src/app/models/pledge/pledge-preview.model';

@Component({
  selector: 'app-pledge-preview',
  templateUrl: './pledge-preview.component.html',
  styleUrls: ['./pledge-preview.component.css']
})
export class PledgePreviewComponent implements OnInit {

  @Input()
  pledgePreview: PledgePreview = <PledgePreview>{};

  constructor() { }

  ngOnInit(): void {
  }

}
