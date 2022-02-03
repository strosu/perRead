import { state } from "@angular/animations";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { map, catchError, Observable } from "rxjs";
import { throwError, NEVER } from 'rxjs';
import { TokenStorageService } from "../services/token-storage.service";

const TOKEN_HEADER_KEY = 'Authorization';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private tokenService: TokenStorageService, private router: Router) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> 
    {
        var request = req;
        var token = this.tokenService.getToken();

        if (token != null) {
            request = req.clone({ headers: req.headers.append(TOKEN_HEADER_KEY, 'Bearer ' + token)});
        }

        return next.handle(request).pipe(catchError(error => {
            if (!!error.status && error.status === 401) {
              this.router.navigate(['/login'], { queryParams: { returnUrl: this.router.routerState.snapshot.url }, replaceUrl: true})
            }
            
            console.log(error);
            return throwError(() => error);
          }));
    }
}

export const authInterceptorProviders = [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ];