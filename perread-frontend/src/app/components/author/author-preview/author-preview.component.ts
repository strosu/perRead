import { Component, Input, OnInit } from '@angular/core';
import { AuthorPreview } from 'src/app/models/author-preview.model';

@Component({
  selector: 'app-author-preview',
  templateUrl: './author-preview.component.html',
  styleUrls: ['./author-preview.component.css']
})
export class AuthorPreviewComponent implements OnInit {

  @Input()
  authorPreview: AuthorPreview = <AuthorPreview>{};

  constructor() { }

  ngOnInit(): void {
    // console.log(`got a new instance ${this.authorPreview.authorName}`);
  }

}
