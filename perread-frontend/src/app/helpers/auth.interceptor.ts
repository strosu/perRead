import { state } from "@angular/animations";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from "@angular/common/http";
import { EventEmitter, Injectable, Output } from "@angular/core";
import { Router } from "@angular/router";
import { map, catchError, Observable } from "rxjs";
import { throwError, NEVER } from 'rxjs';
import { TokenStorageService } from "../services/token-storage.service";

const TOKEN_HEADER_KEY = 'Authorization';

@Injectable({
  providedIn: 'root'
})

export class AuthInterceptor implements HttpInterceptor {

    @Output() onRequest: EventEmitter<any> = new EventEmitter();

    constructor(private tokenService: TokenStorageService, private router: Router) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> 
    {
        var request = req;
        var token = this.tokenService.getToken();

        // if (req.url.startsWith('https://localhost:7176/article')) {
        //   this.onRequest.emit(null);
        // }

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