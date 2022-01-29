import { Component, Input, OnInit } from '@angular/core';
import { Author } from 'src/app/models/author.model';

@Component({
  selector: 'app-user-nav-menu',
  templateUrl: './user-nav-menu.component.html',
  styleUrls: ['./user-nav-menu.component.css']
})
export class UserNavMenuComponent implements OnInit {

  @Input()
  get author(): Author {return this._author}
  set author(author:Author) {
    this._author = author;
  }
  // author: Author = {};
  
  private _author={};

  constructor() { }

  ngOnInit(): void {
  }
}
