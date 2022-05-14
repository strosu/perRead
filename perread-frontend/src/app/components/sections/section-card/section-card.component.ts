import { Component, Input, OnInit } from '@angular/core';
import { SectionPreview } from 'src/app/models/section/section-preview.model';

@Component({
  selector: 'app-section-card',
  templateUrl: './section-card.component.html',
  styleUrls: ['./section-card.component.css']
})
export class SectionCardComponent implements OnInit {

  @Input()
  sectionPreview: SectionPreview = <SectionPreview>{};

  constructor() { }

  ngOnInit(): void {
  }

}
