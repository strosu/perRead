import { Injectable } from '@angular/core';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  constructor() { }

  signout() : void {
    window.sessionStorage.clear();
  }

  saveToken(tokenJson: any): void {
    var token = tokenJson.token;

    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  getToken(): string | null{
    return window.sessionStorage.getItem(TOKEN_KEY);
  }

  saveUser(tokenJson: any) : void {
    // This is the token in json format, do something with it before storing
    
    var token: string = tokenJson.token;
    var userObject = atob(token.split('.')[1]);
    
    // var userJson = JSON.stringify(userObject);

    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, userObject);
  }

  getUser() : any {
    const user = window.sessionStorage.getItem(USER_KEY);
    
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }

  getRoles() : any {
    return {};
  }
}
