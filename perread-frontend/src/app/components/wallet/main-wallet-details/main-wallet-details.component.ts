import { Component, OnInit } from '@angular/core';
import { Wallet } from 'src/app/models/wallet/wallet.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-main-wallet-details',
  templateUrl: './main-wallet-details.component.html',
  styleUrls: ['./main-wallet-details.component.css']
})
export class MainWalletDetailsComponent implements OnInit {

  mainWallet: Wallet = <Wallet>{};
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getCurrentUserMainWallet().subscribe({
      next: data => {
        console.log(data);
        this.mainWallet = data;
      },
      error: err => console.log(err)
    });
  }

}
