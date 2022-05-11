import { Component, Input, OnInit } from '@angular/core';
import { AuthorPreview } from 'src/app/models/author/author-preview.model';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css']
})
export class AuthorListComponent implements OnInit {

  @Input()
  authorsList: AuthorPreview[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
