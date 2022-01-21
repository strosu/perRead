import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  returnUrl: string = '';

  constructor(
    private authService: AuthService, 
    private tokenService: TokenStorageService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    
    this.returnUrl = String(this.route.snapshot.queryParams['returnUrl']);

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
        
          this.router.navigate([this.returnUrl]);
      },
      err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    );
  }

}
