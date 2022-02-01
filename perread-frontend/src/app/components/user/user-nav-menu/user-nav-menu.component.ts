import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorPreview } from 'src/app/models/author-preview.model';
import { UserPreview } from 'src/app/models/user/user-preview.model';
import { AuthorsService } from 'src/app/services/authors.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-nav-menu',
  templateUrl: './user-nav-menu.component.html',
  styleUrls: ['./user-nav-menu.component.css']
})
export class UserNavMenuComponent implements OnInit {

  user?: UserPreview;
  
  constructor(private tokenService: TokenStorageService,
    private usersService: UserService) { }

  ngOnInit(): void {
    this.usersService.getCurrentUserPreview().subscribe(
      {
        next: data => {
          this.user = data;
        },
        error: err => console.log(err)
      }
    );
  }

  logout(): void {
    this.tokenService.signout();
  }
}
