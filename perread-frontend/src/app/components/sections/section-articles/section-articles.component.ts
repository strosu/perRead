import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Section, SectionSubscriptionStatus } from 'src/app/models/section/section.model';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'app-section-articles',
  templateUrl: './section-articles.component.html',
  styleUrls: ['./section-articles.component.css']
})
export class SectionArticlesComponent implements OnInit {

  @Input()
  sectionId: string = '';
  sectionWithArticles: Section = <Section>{};
  subscribedSections = new FormControl();
  
  showFeeds: boolean = false;

  @ViewChild('sectionsSelect') mySelect: any;

  constructor(private sectionsService: SectionsService) {
   }

  ngOnInit(): void {
    this.sectionsService.getSection(this.sectionId).subscribe(
      {
        next: data => {
          console.log(data);
          this.sectionWithArticles = data;
          this.subscribedSections.setValue(this.sectionWithArticles.feedSubscriptionStatuses.filter(x => x.isSubscribedToSection));
          this.showFeeds = this.sectionWithArticles.feedSubscriptionStatuses.length > 0;
        },
        error: err => console.log(err)
      }
    );
  }

  okClicked() {
    // this.valueSelected = this.subscribedSections.value && this.subscribedSections.value.toString();
    // console.log(this.valueSelected);

    let selectedSections = <SectionSubscriptionStatus[]>this.subscribedSections.value;

    this.sectionsService.addSectionToFeeds(this.sectionId, selectedSections.map(x => x.feed.feedId)).subscribe({
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
