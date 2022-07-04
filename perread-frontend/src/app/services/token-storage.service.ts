import { EventEmitter, Injectable, Output } from '@angular/core';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  @Output() onLogin: EventEmitter<any> = new EventEmitter();
  @Output() onLogout: EventEmitter<any> = new EventEmitter();
  
  constructor() { }

  signout() : void {
    window.sessionStorage.clear();
    this.onLogout.emit(null);
  }

  saveToken(tokenJson: any): void {
    var token = tokenJson.token;

    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
    this.onLogin.emit(null)
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

  getUserId() : string {
    var json = this.getUser();
    return json.sub;
  }

  getRoles() : any {
    return {};
  }
}
