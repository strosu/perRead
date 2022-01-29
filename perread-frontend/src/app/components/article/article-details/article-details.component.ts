import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Article } from 'src/app/models/article/article.model';
import { ArticlesService } from 'src/app/services/articles.service';
import { UriService } from 'src/app/services/uri.service';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css']
})
export class ArticleDetailsComponent implements OnInit {

  article?: Article;
  articleImagePath: string = '';

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticlesService,
    private uriService: UriService
  ) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.articleService.get(id).subscribe(
      {
        next: data => {
          console.log(data);
          this.article = data;
          this.articleImagePath = this.uriService.getStaticFileUri(this.article.articleImageUrl);

        },
        error: err => console.log(err)
      }
    );
  }

}
