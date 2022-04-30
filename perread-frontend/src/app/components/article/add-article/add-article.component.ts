import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ArticleCommand, ArticleImage } from 'src/app/models/article/article-command.model';
import { SectionPreview } from 'src/app/models/section/section-preview.model';
import { Section } from 'src/app/models/section/section.model';
import { ArticlesService } from 'src/app/services/articles.service';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.css']
})

export class AddArticleComponent implements OnInit {

  imageError?: string;
  
  get isImageSaved(): boolean {
    return this.articleCommand.articleImage.base64Encoded != '';
  };

  tags: string = '';
  articleCommand: ArticleCommand = new ArticleCommand();
  sections: SectionPreview[] = [];
  selectedSections = new FormControl();

  constructor(
    private articleService: ArticlesService, 
    private sectionService: SectionsService,
    private router: Router) { }

  ngOnInit(): void {
    this.sectionService.listSections().subscribe(
      {
        next: data => {
          console.log(data);
          this.sections = data;
        }
      }
    );
  }

  saveArticle(): void {
    
    let selected: SectionPreview[];
    selected = this.selectedSections.value;

    this.articleCommand.sectionIds = selected.map(x => x.sectionId);

    this.articleCommand.tags = this.tags?.split(",");
    
    this.articleService.create(this.articleCommand)
      .subscribe(
        response => {
          console.log(response);
          this.router.navigate(['/articles'], { replaceUrl: true });
        }
      );
  }

  fileChangeEvent(fileInput: any): any {
    this.imageError = '';

    if (fileInput.target.files && fileInput.target.files[0]) {

      var file = fileInput.target.files[0];

      // Size Filter Bytes
      const max_size = 20971520;
      const allowed_types = ['image/png', 'image/jpeg'];
      const max_height = 15200;
      const max_width = 25600;

      if (file > max_size) {
        this.imageError =
          'Maximum size allowed is ' + max_size / 1000 + 'Mb';

        return false;
      }

      if (!allowed_types.includes(file.type)) {
        this.imageError = 'Only Images are allowed ( JPG | PNG )';
        return false;
      }

      this.articleCommand.articleImage.fileName = file.name;

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
            this.articleCommand.articleImage.base64Encoded = e.target.result;
            // this.isImageSaved = true;
            // this.previewImagePath = imgBase64Path;
            return true;
          }
        };
      };

      reader.readAsDataURL(fileInput.target.files[0]);
    }
  }

  removeImage() {
    this.articleCommand.articleImage = new ArticleImage();
    // this.isImageSaved = false;
  }

}
