import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-section-page',
  templateUrl: './section-page.component.html',
  styleUrls: ['./section-page.component.css']
})
export class SectionPageComponent implements OnInit {

  sectionId: string = '';

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.sectionId = String(this.route.snapshot.paramMap.get('id'));
  }

}
