import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorPreview } from 'src/app/models/author-preview.model';
import { AuthorsService } from 'src/app/services/authors.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-user-nav-menu',
  templateUrl: './user-nav-menu.component.html',
  styleUrls: ['./user-nav-menu.component.css']
})
export class UserNavMenuComponent implements OnInit {

  author?: AuthorPreview;
  
  constructor(private tokenService: TokenStorageService,
    private router: Router,
    private authorsService: AuthorsService) { }

  ngOnInit(): void {
    this.authorsService.getCurrentAuthor().subscribe(
      {
        next: data => {
          this.author = data;
        },
        error: err => console.log(err)
      }
    );
  }

  logout(): void {
    this.tokenService.signout();
    this.router.navigate(["/"]);
  }
}
