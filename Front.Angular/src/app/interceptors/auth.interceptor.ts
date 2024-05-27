import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {

        if (!req.url.includes('/Login') && !req.url.includes('/Logout') && error.status === 401) {
          if (error.error && error.error.message) {
            if (error.error.message === 'Token expirado') {
              this.router.navigate(['/login']);
            }
          } else {
            this.router.navigate(['/login']);
          }
        }
        return throwError(error);
      })
    );
  }
}
