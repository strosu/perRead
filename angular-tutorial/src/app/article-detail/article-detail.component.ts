import { Article } from '../article';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { MessageService } from '../message.service';
import { ArticleService } from '../article.service';
import {Location} from '@angular/common';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})

export class ArticleDetailComponent implements OnInit {

  @Input() article?: Article;

  constructor(
    private messageService: MessageService,
    private articleService: ArticleService,
    private location: Location,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadArticle();
  }

  async loadArticle() {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.messageService.add(`Loading article number ${id}`);

    this.article = await this.articleService.getArticle(id);
  }

  goBack() {
    this.location.back();
  }

}
