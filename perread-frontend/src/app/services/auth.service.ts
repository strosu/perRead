import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const authEndpoint= "https://localhost:7176";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(private httpClient: HttpClient) { }

  login(username: string, password: string) : Observable<any> {
    return this.httpClient.post(`${authEndpoint}/login`, {
      username: username,
      password: password
    });
  }

  register(username: string, password: string, email?: string): Observable<any> {
    return this.httpClient.post(`${authEndpoint}/register`, {
      username: username,
      password: password,
      email: email
    }, httpOptions);
  }
}
