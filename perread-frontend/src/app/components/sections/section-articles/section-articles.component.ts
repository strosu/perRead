import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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
  sectionWithArticles: Section = <Section>{};

  form: FormGroup = <FormGroup>{};

  constructor(private sectionsService: SectionsService) {
   }

  ngOnInit(): void {

    this.sectionsService.getSection(this.sectionPreview.sectionId).subscribe(
      {
        next: data => {
          console.log(data);
          this.sectionWithArticles = data;
          this.form = new FormGroup({
            project: new FormControl(this.sectionWithArticles.feedSubscriptionStatuses)      
          });
        },
        error: err => console.log(err)
      }
    );
  }

  okClicked() {
    let current = this.form.controls['project'].value;
    console.log(current);
    // this.matSelect.close()
  }

  cancelClicked() {
    // this.form.controls['project'].setValue(this.last_selection)
    // this.matSelect.close()
  }

  onOptionsSelected($event : any){
    console.log($event);
   }
}
