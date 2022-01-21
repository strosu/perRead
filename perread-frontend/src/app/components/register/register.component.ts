import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: any = {
    username: null,
    password: null,
    email: null
  };

  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private loginService: LoginService) { }

  ngOnInit(): void {
  }

  onSubmit() : void {
    const { username, password, email } = this.form;

   this.authService.register(username, password, email).subscribe({
     next: _ => {
      this.loginService.signIn(username, password, '/');
     },
     error: err => console.log(err.error)
   }); 
  }
}
