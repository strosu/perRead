import { Component, OnInit } from '@angular/core';
import { ArticleUnlockInfo } from 'src/app/models/user/article-unlock-info';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-unlocked',
  templateUrl: './user-unlocked.component.html',
  styleUrls: ['./user-unlocked.component.css']
})
export class UserUnlockedComponent implements OnInit {

  articleUnlockInfos: ArticleUnlockInfo[] = [];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getCurrentUserUnlockedArticles().subscribe(
      {
        next: data => {
          console.log(data);
          this.articleUnlockInfos = data;
        },

        error : err => console.log(err)
      }
    );
  }

  save() : void {
    this.userService.updateCurrentUserUnlockedArticles(this.articleUnlockInfos.map(x => x.articleUnlockId)).subscribe(
      {
        next: data => console.log(data),
        error: err => console.log(err)
      }
    );
  }

}
