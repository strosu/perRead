import { Component, Input, OnInit } from '@angular/core';
import { UrlHelper } from 'src/app/helpers/url-helper.model';
import { TransactionPreview } from 'src/app/models/wallet/transaction-preview.model';

@Component({
  selector: 'app-pledge-cancelled-comment',
  templateUrl: './pledge-cancelled-comment.component.html',
  styleUrls: ['./pledge-cancelled-comment.component.css']
})
export class PledgeCancelledCommentComponent implements OnInit {

  @Input()
  transactionPreview: TransactionPreview = <TransactionPreview>{};
  
  pledgeUrl: string = '';

  constructor() { }

  ngOnInit(): void {
    this.pledgeUrl = UrlHelper.getPledgeUrl(this.transactionPreview.comment);
  }

}
