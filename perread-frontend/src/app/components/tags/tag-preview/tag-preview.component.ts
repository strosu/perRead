import { Component, Input, OnInit } from '@angular/core';
import { TagPreview } from 'src/app/models/tag-preview.model';

@Component({
  selector: 'app-tag-preview',
  templateUrl: './tag-preview.component.html',
  styleUrls: ['./tag-preview.component.css']
})
export class TagPreviewComponent implements OnInit {

  @Input()
  tagPreview?: TagPreview;

  constructor() { }

  ngOnInit(): void {
    // console.log(`got intialized with ${this.tagPreview}`);
  }

}
