import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isLoggedIn = false;
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
