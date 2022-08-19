import { Component, Input, OnInit } from '@angular/core';
import { TransactionPreview } from 'src/app/models/wallet/transaction-preview.model';

@Component({
  selector: 'app-token-withdrawal-comment',
  templateUrl: './token-withdrawal-comment.component.html',
  styleUrls: ['./token-withdrawal-comment.component.css']
})
export class TokenWithdrawalCommentComponent implements OnInit {
  @Input()
  transactionPreview: TransactionPreview = <TransactionPreview>{};
  
  constructor() { }

  ngOnInit(): void {
  }

}
