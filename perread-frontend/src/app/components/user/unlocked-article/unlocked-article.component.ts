import { Component, Input, OnInit } from '@angular/core';
import { ArticleUnlockInfo } from 'src/app/models/user/article-unlock-info';

@Component({
  selector: 'app-unlocked-article',
  templateUrl: './unlocked-article.component.html',
  styleUrls: ['./unlocked-article.component.css']
})

export class UnlockedArticleComponent implements OnInit {

  @Input()
  index: number = -1;

  @Input()
  articleList? : ArticleUnlockInfo[];

  articleUnlockInfo : ArticleUnlockInfo =  <ArticleUnlockInfo>{};

  constructor() { }

  ngOnInit(): void {
    if (this.articleList && this.index >= 0) {
      this.articleUnlockInfo = this.articleList[this.index];      
    }
  }

  remove() : void {
    if (this.index >= 0) {
      this.articleList?.splice(this.index, 1);
    }
  }

}
