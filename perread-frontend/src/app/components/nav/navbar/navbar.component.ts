import { Component, OnInit } from '@angular/core';
import { AuthorPreview } from 'src/app/models/author-preview.model';
import { AuthService } from 'src/app/services/auth.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  
  isLoggedIn = false;
  user?: AuthorPreview;
  
  constructor(private tokenService: TokenStorageService) { }

  ngOnInit(): void {
    this.computeLoggedIn();
    this.tokenService.onLogin.subscribe(_ => this.isLoggedIn = true); // set it manually, don't wait for the token to be set, as this will happen afterwards
    this.tokenService.onLogout.subscribe(_ => this.isLoggedIn = false);
  }

  computeLoggedIn() : void {
    this.isLoggedIn = !!this.tokenService.getToken();
  }
}
