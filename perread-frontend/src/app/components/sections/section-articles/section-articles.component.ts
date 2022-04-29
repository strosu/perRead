import { Component, Input, OnInit } from '@angular/core';
import { SectionPreview } from 'src/app/models/section/section-preview.model';
import { Section } from 'src/app/models/section/section.model';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'app-section-articles',
  templateUrl: './section-articles.component.html',
  styleUrls: ['./section-articles.component.css']
})
export class SectionArticlesComponent implements OnInit {

  @Input()
  sectionPreview: SectionPreview = <SectionPreview>{};

  sectionWithArticles?: Section;

  constructor(private sectionsService: SectionsService) { }

  ngOnInit(): void {
    this.sectionsService.getSection(this.sectionPreview.sectionId).subscribe(
      {
        next: data => {
          console.log(data);
          this.sectionWithArticles = data;
        },
        error: err => console.log(err)
      }
    );
  }

}
