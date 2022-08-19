import { Component, Input, OnInit } from '@angular/core';
import { UrlHelper } from 'src/app/helpers/url-helper.model';
import { TransactionPreview } from 'src/app/models/wallet/transaction-preview.model';

@Component({
  selector: 'app-pledge-created-comment',
  templateUrl: './pledge-created-comment.component.html',
  styleUrls: ['./pledge-created-comment.component.css']
})
export class PledgeCreatedCommentComponent implements OnInit {

  @Input()
  transactionPreview: TransactionPreview = <TransactionPreview>{};
  
  pledgeUrl: string = '';

  constructor() { }

  ngOnInit(): void {
    this.pledgeUrl = UrlHelper.getPledgeUrl(this.transactionPreview.comment);
  }

}
