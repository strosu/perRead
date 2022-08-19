import { Component, Input, OnInit } from '@angular/core';
import { TransactionPreview } from 'src/app/models/wallet/transaction-preview.model';

@Component({
  selector: 'app-article-read-comment',
  templateUrl: './article-read-comment.component.html',
  styleUrls: ['./article-read-comment.component.css']
})
export class ArticleReadCommentComponent implements OnInit {

  @Input()
  transactionPreview: TransactionPreview = <TransactionPreview>{};
  
  articleUrl: string = '';

  constructor() { }

  ngOnInit(): void {
    this.articleUrl = `article/${this.transactionPreview.comment}`
  }
}
