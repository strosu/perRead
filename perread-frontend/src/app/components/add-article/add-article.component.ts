import { Component, OnInit } from '@angular/core';
import { Article } from 'src/app/models/article.model';
import { ArticlesService } from 'src/app/services/articles.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.css']
})

export class AddArticleComponent implements OnInit {

  tags?: string;

  article: Article = {
    title: '',
    content: 'type your content here',
    price: 1
  };

  submitted = false;

  constructor(private articleService: ArticlesService, private tokenService: TokenStorageService) { }

  ngOnInit(): void {
  }

  saveArticle() : void {

    const currentUser = this.tokenService.getUser().userName;

    const data = {
      title: this.article.title,
      content: this.article.content,
      price: this.article.price,
      authors: [currentUser],
      tags: this.tags?.split(",")
    };

    this.articleService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
        }
      );
  }

  newArticle() : void{
    this.submitted = false;
    this.article = {
      title: '',
      content: '',
      price: 1
    }
  }

}
