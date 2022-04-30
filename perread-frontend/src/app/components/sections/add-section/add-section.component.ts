import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SectionCommand } from 'src/app/models/section/section-command.model';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'app-add-section',
  templateUrl: './add-section.component.html',
  styleUrls: ['./add-section.component.css']
})
export class AddSectionComponent implements OnInit {

  sectionComand: SectionCommand = <SectionCommand>{};
  
  constructor(private sectionService: SectionsService, private router: Router) { }

  ngOnInit(): void {
  }

  saveSection() : void {
    this.sectionService.createSection(this.sectionComand).subscribe(
      {
        next: data => {
          console.log(data);
          this.router.navigate(['/section-management'], { replaceUrl: true });
        }
      }
    );
  }

}
