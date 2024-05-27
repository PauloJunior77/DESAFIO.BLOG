import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap, catchError, map } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';
import { LoginModel } from '../Models/LoginModel';
import { RegisterModel } from '../Models/RegisterModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7183/api/Auth';

  constructor(private http: HttpClient, private router: Router) {}

  login(loginModel: LoginModel): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Login`, loginModel).pipe(
      tap(response => {
        if (response && response.token) {
          this.setToken(response.token);
          const user = {
            id: response.user.id,
            email: response.user.email,
            isAdmin: response.user.isAdmin
          };
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(registerModel: RegisterModel): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Register`, registerModel);
  }

  logout(): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Logout`, null).pipe(
      tap(() => this.clearUserData())
    );
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token && !this.isTokenExpired(token);
  }

  getCurrentUser(): any {
    return JSON.parse(localStorage.getItem('currentUser') || '{}');
  }

  getJwtToken(): string | null {
    return localStorage.getItem('token'); 
  }

  verifyToken(): Observable<boolean> {
    const token = this.getToken();

    if (!token) {
      return of(false);
    }

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    });

    return this.http.get<any>(`${this.apiUrl}/VerifyToken`, { headers }).pipe(
      map(() => true),
      catchError(() => {
        this.clearUserData();
        return of(false);
      })
    );
  }

  private setToken(token: string): void {
    localStorage.setItem('token', token);
  }

  private getToken(): string | null {
    return localStorage.getItem('token');
  }

  private isTokenExpired(token: string): boolean {
    const tokenPayload = JSON.parse(atob(token.split('.')[1]));
    const expirationDate = new Date(tokenPayload.exp * 1000);
    return expirationDate <= new Date();
  }

  private setCurrentUser(user: any): void {
    localStorage.setItem('currentUser', JSON.stringify(user));
  }

  get currentUserValue(): any {
    return {
      token: this.getToken(),
      user: this.getCurrentUser()
    };
  }

  private clearUserData(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    this.router.navigate(['/login']);
  }
}
