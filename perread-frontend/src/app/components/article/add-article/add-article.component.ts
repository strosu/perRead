import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ArticleCommand } from 'src/app/models/article/article-command.model';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.css']
})

export class AddArticleComponent implements OnInit {

  imageError?: string;
  isImageSaved?: boolean;

  articleCommand: ArticleCommand = new ArticleCommand();

  submitted = false;

  constructor(private articleService: ArticlesService, private router: Router) { }

  ngOnInit(): void {
  }

  saveArticle() : void {
    const data = {
      title: this.articleCommand.title,
      content: this.articleCommand.content,
      price: this.articleCommand.price,
      tags: this.articleCommand.tags?.split(",")
    };

    this.articleService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
          this.router.navigate(['/articles']);
        }
      );
  }

  fileChangeEvent(fileInput: any): any {
    this.imageError = '';

    if (fileInput.target.files && fileInput.target.files[0]) {
        
      // Size Filter Bytes
        const max_size = 20971520;
        const allowed_types = ['image/png', 'image/jpeg'];
        const max_height = 15200;
        const max_width = 25600;

        if (fileInput.target.files[0].size > max_size) {
            this.imageError =
                'Maximum size allowed is ' + max_size / 1000 + 'Mb';

            return false;
        }

        if (!allowed_types.includes(fileInput.target.files[0].type)) {
            this.imageError = 'Only Images are allowed ( JPG | PNG )';
            return false;
        }
        
        const reader = new FileReader();
        reader.onload = (e: any) => {
            const image = new Image();
            image.src = e.target.result;
            image.onload = rs => {
              // TODO - figure out this stuff for proper error handling
                // var im = rs.currentTarget as Image;
                const img_height = 1; // rs.currentTarget['height'];
                const img_width = 1; //rs.currentTarget['width'];

                console.log(img_height, img_width);

                if (img_height > max_height && img_width > max_width) {
                    this.imageError =
                        'Maximum dimentions allowed ' +
                        max_height +
                        '*' +
                        max_width +
                        'px';
                    return false;
                } else {
                    const imgBase64Path = e.target.result;
                    this.articleCommand.cardImageBase64 = imgBase64Path;
                    this.isImageSaved = true;
                    // this.previewImagePath = imgBase64Path;
                    return true;
                }
            };
        };

        reader.readAsDataURL(fileInput.target.files[0]);
    }
}

removeImage() {
    this.articleCommand.cardImageBase64 = '';
    this.isImageSaved = false;
}

}
