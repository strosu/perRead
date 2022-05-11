import { Component, Input, OnInit } from '@angular/core';
import { TagPreview } from 'src/app/models/tag/tag-preview.model';

@Component({
  selector: 'app-tag-list',
  templateUrl: './tag-list.component.html',
  styleUrls: ['./tag-list.component.css']
})
export class TagListComponent implements OnInit {

  @Input()
  tagList?: TagPreview[] = [];
  
  selectedTag: TagPreview = {};
  currentIndex = -1;
  
  constructor() { }

  ngOnInit(): void {
  }

  setActiveTag(article: TagPreview, currentIndex: number) : void {
    this.selectedTag = article;
    this.currentIndex = currentIndex;
  }

}
