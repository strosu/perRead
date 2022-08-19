import { Component, OnInit } from '@angular/core';
import { Wallet } from 'src/app/models/wallet/wallet.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-escrow-wallet-details',
  templateUrl: './escrow-wallet-details.component.html',
  styleUrls: ['./escrow-wallet-details.component.css']
})
export class EscrowWalletDetailsComponent implements OnInit {

  escrowWallet: Wallet = <Wallet>{};
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getCurrentUserEscrowWallet().subscribe({
      next: data => {
        console.log(data);
        this.escrowWallet = data;
      },
      error: err => console.log(err)
    });
  }
}
