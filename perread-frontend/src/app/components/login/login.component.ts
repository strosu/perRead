import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: any = {
    username : null,
    password: null
  }

  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];
  username: string = '';

  constructor(
    private authService: AuthService, 
    private tokenService: TokenStorageService) { }

  ngOnInit(): void {
    if (this.tokenService.getToken()) {
      this.isLoggedIn = true;
      
      var userJson = this.tokenService.getUser();

      this.roles = userJson.roles;

      this.username = userJson.userName;
    }
  }

  onSubmit() {
    const {username, password } = this.form;
    this.authService.login(username, password).subscribe(
      data => {
          this.tokenService.saveToken(data);
          this.tokenService.saveUser(data);

          this.isLoggedIn = true;
          this.isLoginFailed = false;

          this.roles = this.tokenService.getRoles();
        
          window.location.reload();
      },
      err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    );
  }

}
