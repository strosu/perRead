import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from './services/token-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'perread-frontend';
  isLoggedIn = false;
  userName?: string;

  constructor(private tokenService: TokenStorageService) {  }
  
  ngOnInit(): void {
    this.isLoggedIn = !!this.tokenService.getToken();

    if (this.isLoggedIn) {
      var user = this.tokenService.getUser();
      this.userName = user.userName;
    }
  }

  logout(): void {
    this.tokenService.signout();
    window.location.reload();
  }
}
