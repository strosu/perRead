import { Component, OnInit } from '@angular/core';
import { SectionPreview } from 'src/app/models/section/section-preview.model';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'app-section-management',
  templateUrl: './section-management.component.html',
  styleUrls: ['./section-management.component.css']
})
export class SectionManagementComponent implements OnInit {

sections: SectionPreview[] = [];

  constructor(private sectionsService: SectionsService) { }

  ngOnInit(): void {
    this.sectionsService.listSections().subscribe({
      next: data => {
        console.log(data);
        this.sections = data;
      }
    });
  }

}
