import { Component, Input, OnInit } from '@angular/core';
import { TransactionPreview } from 'src/app/models/wallet/transaction-preview.model';

@Component({
  selector: 'app-token-purchase-comment',
  templateUrl: './token-purchase-comment.component.html',
  styleUrls: ['./token-purchase-comment.component.css']
})
export class TokenPurchaseCommentComponent implements OnInit {

  @Input()
  transactionPreview: TransactionPreview = <TransactionPreview>{};
  
  constructor() { }

  ngOnInit(): void {
  }

}
