import { Component, Input, OnInit } from '@angular/core';
import { PledgePreview } from 'src/app/models/pledge/pledge-preview.model';

@Component({
  selector: 'app-pledge-list',
  templateUrl: './pledge-list.component.html',
  styleUrls: ['./pledge-list.component.css']
})
export class PledgeListComponent implements OnInit {

  @Input()
  pledgePreviews: PledgePreview[] = [];
  
  constructor() { }

  ngOnInit(): void {
  }

}
