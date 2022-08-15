import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { ArticleOwner } from 'src/app/models/author/article-owner.model';
import { OwnerCollection } from 'src/app/models/author/article-ownership.model';
import { AuthorPreview } from 'src/app/models/author/author-preview.model';
import { OwnerDescription, UpdateOwnershipCommand } from 'src/app/models/author/owner-description.model';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-article-owners',
  templateUrl: './article-owners.component.html',
  styleUrls: ['./article-owners.component.css']
})
export class ArticleOwnersComponent implements OnInit {

  id: string = '';
  error: string = '';

  ownerCollection : OwnerCollection = <OwnerCollection>{};
  constructor(
    private route: ActivatedRoute,
    private articleService: ArticlesService,
  ) { }

  displayedColumns = ['name', 'id', 'percent', 'editable', 'visible'];
  dataSource = new MatTableDataSource(this.ownerCollection.owners);

  ngOnInit(): void {
    this.id = String(this.route.snapshot.paramMap.get('id'));
    this.articleService.getOwners(this.id).subscribe({
      next: data => {
        console.log(data);
        this.setDataAndRefreshTable(data);
      },
      error : err => console.log(err)
    });
  }

  saveOwners() : void {
    let command = new UpdateOwnershipCommand();
    command.owners = this.ownerCollection.owners.map(x => ({ authorId: x.ownerPreview.authorId, ownershipPercent: x.ownershipPercent}));
    // this.error = "gaaah";
    this.articleService.updateOwners(this.id, command).subscribe({
      next : data => {
        console.log(data);
        this.setDataAndRefreshTable(data);
      },
      error : err => {
        console.log(err);
        this.error = err;
      }
    })
  }

  setDataAndRefreshTable(data: OwnerCollection) : void {
    this.ownerCollection = data;
    this.dataSource = new MatTableDataSource(this.ownerCollection.owners);
  }

  addRow() : void {
    let newRow = new ArticleOwner();
    newRow.ownerPreview = new AuthorPreview();
    newRow.canBeEdited = true;
    newRow.isUserFacing = true;
    this.ownerCollection.owners.push(newRow);
    this.dataSource = new MatTableDataSource(this.ownerCollection.owners);
  }
}

