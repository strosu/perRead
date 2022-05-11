import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Author } from 'src/app/models/author/author.model';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-author-details',
  templateUrl: './author-details.component.html',
  styleUrls: ['./author-details.component.css']
})
export class AuthorDetailsComponent implements OnInit {

  author: Author = <Author>{};
  
  constructor(
    private route: ActivatedRoute,
    private authorService: AuthorsService) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.authorService.getAuthor(id).subscribe(
      {
        next: data => {
          console.log(data);
          this.author = data;
        },
        error: err => console.log(err)
      }
    );
  }

}
