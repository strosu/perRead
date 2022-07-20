import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-withdraw-tokens',
  templateUrl: './withdraw-tokens.component.html',
  styleUrls: ['./withdraw-tokens.component.css']
})

export class WithdrawTokensComponent implements OnInit {

  tokensToWithdraw: number = 0;
  tokensInAccount: number = 0;
  moneyToReceive: number = this.tokensToWithdraw * 400;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.userService.getCurrentUserPreview().subscribe({
      next: data => {
        console.log(data);
        this.tokensInAccount = data.readingWallet?.tokenAmount;
      }
    });
  }

computeAmount() {
  this.moneyToReceive = this.tokensToWithdraw * 0.8 / 100;
}

  withdraw() {
    this.userService.withdrawTokens(this.tokensToWithdraw).subscribe({
      next: data => {
        console.log(data);
        this.tokensInAccount = data;
        this.userService.onUpdatedUserInformation.emit(null);
        this.router.navigate(['/'], { replaceUrl: true });
      }
    });
  }

}
