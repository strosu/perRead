import { Component, OnInit } from '@angular/core';
import { Article } from 'src/app/models/article.model';
import { ArticlesService } from 'src/app/services/articles.service';

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

  constructor(private articleService: ArticlesService) { }

  ngOnInit(): void {
  }

  saveArticle() : void {
    const data = {
      title: this.article.title,
      content: this.article.content,
      price: this.article.price,
      author: "author", // replace with current logged in used somehow
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
