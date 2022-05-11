import { Component, Input, OnInit } from '@angular/core';
import { AuthorPreview } from 'src/app/models/author/author-preview.model';

@Component({
  selector: 'app-author-card',
  templateUrl: './author-card.component.html',
  styleUrls: ['./author-card.component.css']
})

export class AuthorCardComponent implements OnInit {

  @Input()
  authorPreview: AuthorPreview = <AuthorPreview>{};
  
  constructor() { }

  ngOnInit(): void {
  }

}
