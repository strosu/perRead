import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Tag } from 'src/app/models/tag/tag.model';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-tag-details',
  templateUrl: './tag-details.component.html',
  styleUrls: ['./tag-details.component.css']
})
export class TagDetailsComponent implements OnInit {

  currentTag?: Tag;

  constructor(
    private tagService: TagService, 
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.tagService.getTag(id).subscribe({
      next: data => {
        this.currentTag = data;
      },
      error: err => console.log(err)
    });
  }

}
