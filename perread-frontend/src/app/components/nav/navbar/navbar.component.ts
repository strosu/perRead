import { Component, OnInit } from '@angular/core';
import { AuthInterceptor } from 'src/app/helpers/auth.interceptor';
import { UserPreview } from 'src/app/models/user/user-preview.model';
import { ArticlesService } from 'src/app/services/articles.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  
  isLoggedIn = false;
  user?: UserPreview;
  
  constructor(private tokenService: TokenStorageService,
    private usersService: UserService,
    private articleService: ArticlesService,
    private httpInj: AuthInterceptor) { }

  ngOnInit(): void {
    this.computeLoggedIn();
    this.tokenService.onLogin.subscribe(_ => this.onLogIn()); // set it manually, don't wait for the token to be set, as this will happen afterwards
    this.tokenService.onLogout.subscribe(_ => this.isLoggedIn = false);
    this.articleService.onArticleServed.subscribe(_ => this.getUserPreview());
    this.usersService.onUpdatedUserInformation.subscribe(_ => this.getUserPreview());

    if (this.isLoggedIn) {
      this.getUserPreview();
    }

    this.httpInj.onRequest.subscribe(_ => this.getUserPreview());

  }

  computeLoggedIn() : void {
    this.isLoggedIn = !!this.tokenService.getToken();
  }

  onLogIn(): void {
    this.isLoggedIn = true;
    this.getUserPreview();
  }

  getUserPreview(): any {
    this.usersService.getCurrentUserPreview().subscribe(
      {
        next: data => {
          this.user = data;
        },
        error: err => console.log(err)
      }
    );  
  }
}
