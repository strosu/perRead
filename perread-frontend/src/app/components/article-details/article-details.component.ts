import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Article } from 'src/app/models/article.model';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css']
})
export class ArticleDetailsComponent implements OnInit {

  article?: Article;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticlesService
  ) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.articleService.get(id).subscribe(
      {
        next: data => {
          console.log(data);
          this.article = data;
        },
        error: err => console.log(err)
      }
    );
  }

}
