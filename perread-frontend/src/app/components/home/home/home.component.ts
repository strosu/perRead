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
  }
  
  computeLoggedIn() : void {
    this.isLoggedIn = !!this.tokenService.getToken();
  }
}
