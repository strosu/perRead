import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { TokenStorageService } from './token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private authService: AuthService,
    private tokenService: TokenStorageService,
    private router: Router) { }

  signIn(username: string, password: string, navigateTo: string) : void {
    // login and get the token
    this.authService.login(username, password).subscribe({
      next: data => {
        this.tokenService.saveToken(data);
        this.tokenService.saveUser(data);
          this.router.navigate([navigateTo], {replaceUrl: true});
       },
       error: err => console.log(err.error.message)
    });
  }
}
