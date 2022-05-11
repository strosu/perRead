import { Component, Input, OnInit } from '@angular/core';
import { SectionPreview } from 'src/app/models/section/section-preview.model';

@Component({
  selector: 'app-section-preview',
  templateUrl: './section-preview.component.html',
  styleUrls: ['./section-preview.component.css']
})
export class SectionPreviewComponent implements OnInit {

  @Input()
  sectionPreview: SectionPreview = <SectionPreview>{};

  constructor() { }

  ngOnInit(): void {
  }

}
