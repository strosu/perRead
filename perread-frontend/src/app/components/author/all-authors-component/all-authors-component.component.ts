import { Component, OnInit } from '@angular/core';
import { AuthorPreview } from 'src/app/models/author/author-preview.model';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-all-authors-component',
  templateUrl: './all-authors-component.component.html',
  styleUrls: ['./all-authors-component.component.css']
})
export class AllAuthorsComponentComponent implements OnInit {

  authorPreviews: AuthorPreview[] = [];

  constructor(private authorsService: AuthorsService) { }

  ngOnInit(): void {
    this.authorsService.getAll().subscribe({
      next: data => {
        console.log(data);
        this.authorPreviews = data;
      }
    });
  }

}
