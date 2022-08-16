import { Component, Input, OnInit } from '@angular/core';
import { TransactionPreview } from 'src/app/models/wallet/transaction-preview.model';

@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.css']
})
export class TransactionsListComponent implements OnInit {

  @Input()
  transactionList: TransactionPreview[] = [];
  
  total: number = 0;

  displayedColumns: string[] = ['transaction-id', 'source', 'destination', 'amount', 'type', 'date', 'comment'];
  
  constructor() {
   }

  ngOnInit(): void {
  }

  ngOnChanges() : void {
    this.total = this.transactionList?.map(x => x.tokenAmount).reduce((x, y) => x + y);
  }

  getLabel(transaction: TransactionPreview) : string {
    return transaction?.comment;
  }
}
