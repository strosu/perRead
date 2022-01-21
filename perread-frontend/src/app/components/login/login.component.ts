import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LoginService } from 'src/app/services/login.service';
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
    private tokenService: TokenStorageService,
    private route: ActivatedRoute,
    private loginService: LoginService,
    private router: Router) { }

  ngOnInit(): void {
    
    this.returnUrl = String(this.route.snapshot.queryParams['returnUrl']);

    if (this.tokenService.getToken()) {
      this.isLoggedIn = true;
      
      var userJson = this.tokenService.getUser();

      this.roles = userJson.roles;

      this.username = userJson.userName;

      // this.router.navigate([this.returnUrl]);
    }
  }

  onSubmit() {
    const {username, password } = this.form;
    this.loginService.signIn(username, password, this.returnUrl);
  }

}
