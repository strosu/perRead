import { Component, OnInit } from '@angular/core';
import { Author } from './models/author.model';
import { AuthorsService } from './services/authors.service';
import { TokenStorageService } from './services/token-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'perread-frontend';
  isLoggedIn = false;
  user: Author = {};

  constructor(private tokenService: TokenStorageService,
    private authorsService: AuthorsService) {  }
  
  ngOnInit(): void {
    this.isLoggedIn = !!this.tokenService.getToken();

    if (this.isLoggedIn) {
      this.authorsService.getCurrentAuthor().subscribe(
        {
          next: data => {
            this.user = data;
          },
          error: err => console.log(err)
        }
      );
    }
  }

  logout(): void {
    this.tokenService.signout();
    window.location.reload();
  }
}
