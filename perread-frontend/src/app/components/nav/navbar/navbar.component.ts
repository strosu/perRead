import { Component, OnInit } from '@angular/core';
import { UserPreview } from 'src/app/models/user/user-preview.model';
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
    private usersService: UserService) { }

  ngOnInit(): void {
    this.computeLoggedIn();
    this.tokenService.onLogin.subscribe(_ => this.onLogIn()); // set it manually, don't wait for the token to be set, as this will happen afterwards
    this.tokenService.onLogout.subscribe(_ => this.isLoggedIn = false);

    if (this.isLoggedIn) {
      this.getUserPreview();
    }
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
