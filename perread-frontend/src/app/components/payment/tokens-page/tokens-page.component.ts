import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-tokens-page',
  templateUrl: './tokens-page.component.html',
  styleUrls: ['./tokens-page.component.css']
})
export class TokensPageComponent implements OnInit {

  tokenAmount: number = 0;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getCurrentUserPreview().subscribe({
      next: data => {
        this.tokenAmount = data.readingTokens
      }
    });
  }

}
