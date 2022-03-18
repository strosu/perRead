import { Component, Input, OnInit } from '@angular/core';
import { AuthInterceptor } from 'src/app/helpers/auth.interceptor';
import { UserPreview } from 'src/app/models/user/user-preview.model';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-nav-menu',
  templateUrl: './user-nav-menu.component.html',
  styleUrls: ['./user-nav-menu.component.css']
})
export class UserNavMenuComponent implements OnInit {

  @Input()
  user?: UserPreview;
  
  constructor(private tokenService: TokenStorageService,
    private usersService: UserService,
    private httpInj: AuthInterceptor) { }

  ngOnInit(): void {
  }

  logout(): void {
    this.tokenService.signout();
  }

  addTokens(): void {
    this.usersService.addMoreTokens().subscribe( {
      next: data => {
        if (this.user?.readingTokens) {
          this.user.readingTokens = data;
        }
      }
    });
  }
}
