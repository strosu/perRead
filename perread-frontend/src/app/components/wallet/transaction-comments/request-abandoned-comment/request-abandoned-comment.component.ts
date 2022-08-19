import { Component, Input, OnInit } from '@angular/core';
import { UrlHelper } from 'src/app/helpers/url-helper.model';
import { TransactionPreview } from 'src/app/models/wallet/transaction-preview.model';

@Component({
  selector: 'app-request-abandoned-comment',
  templateUrl: './request-abandoned-comment.component.html',
  styleUrls: ['./request-abandoned-comment.component.css']
})
export class RequestAbandonedCommentComponent implements OnInit {
  @Input()
  transactionPreview: TransactionPreview = <TransactionPreview>{};
  
  requestUrl: string = '';
  constructor() { }

  ngOnInit(): void {
    this.requestUrl = UrlHelper.getRequestUrl(this.transactionPreview.comment);
  }
}
