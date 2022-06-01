import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SectionCommand } from 'src/app/models/section/section-command.model';
import { SectionPreview } from 'src/app/models/section/section-preview.model';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'app-section-details',
  templateUrl: './section-details.component.html',
  styleUrls: ['./section-details.component.css']
})
export class SectionDetailsComponent implements OnInit {

  sectionPreview: SectionPreview = <SectionPreview>{};

  constructor(private sectionService: SectionsService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.sectionService.getSectionDetails(id).subscribe({
      next : data => {
        console.log(data);
        this.sectionPreview = data;
      },
      error: err => console.log(err)
    }
    );
  }

  updateSection() : any {
    this.sectionService.updateSection(this.sectionPreview.sectionId, this.sectionPreview).subscribe({
      next: data => {
        console.log(data);
        this.router.navigate(['/section-management'], { replaceUrl: true });
      },
      error : err => console.log(err)
    });
  }

  deleteSection() : any {
    this.sectionService.deleteSection(this.sectionPreview.sectionId).subscribe({
      next : data => {
        console.log(data);
        this.router.navigate(['/section-management'], { replaceUrl: true });
      }
    });
  }

}
