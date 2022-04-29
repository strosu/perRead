import { Component, Input, OnInit } from '@angular/core';
import { SectionPreview } from 'src/app/models/section/section-preview.model';

@Component({
  selector: 'app-section-list',
  templateUrl: './section-list.component.html',
  styleUrls: ['./section-list.component.css']
})
export class SectionListComponent implements OnInit {

  @Input()
  sectionPreviews?: SectionPreview[];

  constructor() { }

  ngOnInit(): void {
  }

}
