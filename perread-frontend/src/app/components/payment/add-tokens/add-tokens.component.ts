import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add-tokens',
  templateUrl: './add-tokens.component.html',
  styleUrls: ['./add-tokens.component.css']
})
export class AddTokensComponent implements OnInit {

  currentTokens: number = 0;
  ammountToPay: number = 0;
  tokensToBuy: number = 0;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.userService.getCurrentUserPreview().subscribe({
      next: data => {
        this.currentTokens = data.readingTokens
      }
    });
  }

  addToCart(tokensToBuy: number, ammountToPay: number) {
    this.tokensToBuy += tokensToBuy;
    this.ammountToPay += ammountToPay;
  }

  buy() {
    this.userService.addMoreTokens(this.tokensToBuy).subscribe({
      next: data => {
        console.log(data);
        this.userService.onUpdatedUserInformation.emit(null);
        this.router.navigate(['/'], { replaceUrl: true });
      },
      error: err => console.log(err)
    });
  }
}
