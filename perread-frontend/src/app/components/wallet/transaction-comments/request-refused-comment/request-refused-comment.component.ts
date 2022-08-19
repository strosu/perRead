import { Component, Input, OnInit } from '@angular/core';
import { UrlHelper } from 'src/app/helpers/url-helper.model';
import { TransactionPreview } from 'src/app/models/wallet/transaction-preview.model';

@Component({
  selector: 'app-request-refused-comment',
  templateUrl: './request-refused-comment.component.html',
  styleUrls: ['./request-refused-comment.component.css']
})
export class RequestRefusedCommentComponent implements OnInit {
  @Input()
  transactionPreview: TransactionPreview = <TransactionPreview>{};
  
  requestUrl: string = '';
  constructor() { }

  ngOnInit(): void {
    this.requestUrl = UrlHelper.getRequestUrl(this.transactionPreview.comment);
  }
}
