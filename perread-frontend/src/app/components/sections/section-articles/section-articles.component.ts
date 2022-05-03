import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { SectionCommand } from 'src/app/models/section/section-command.model';
import { SectionPreview } from 'src/app/models/section/section-preview.model';
import { Section, SectionSubscriptionStatus } from 'src/app/models/section/section.model';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'app-section-articles',
  templateUrl: './section-articles.component.html',
  styleUrls: ['./section-articles.component.css']
})
export class SectionArticlesComponent implements OnInit {

  @Input()
  sectionPreview: SectionPreview = <SectionPreview>{};
  sectionWithArticles: Section = <Section>{};

  subscribedSections = new FormControl();

  valueSelected: string = '';
  
  @ViewChild('sectionsSelect') mySelect: any;

  constructor(private sectionsService: SectionsService) {
   }

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

  okClicked() {
    this.valueSelected = this.subscribedSections.value && this.subscribedSections.value.toString();
    console.log(this.valueSelected);

    let selectedSections = <SectionSubscriptionStatus[]>this.subscribedSections.value;

    this.sectionsService.addSectionToFeeds(this.sectionPreview.sectionId, selectedSections.map(x => x.feed.feedId)).subscribe({
      next: data => {
        console.log(data);
        this.mySelect.close()
      },
      error : err => console.log(err)
    });
  }

  cancelClicked() {
    this.mySelect.close();
  }

  onOptionsSelected($event : any){
    console.log($event);
   }
}
