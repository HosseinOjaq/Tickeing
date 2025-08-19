import { Injectable, inject, signal  } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environment';
import { CookieService } from './cookie.service';
import { Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private cookieService = inject(CookieService);
  private router = inject(Router);
  private apiUrl = environment.apiUrl;
  isAuthenticated = signal<boolean>(false);

  constructor() {
    this.checkAuth();        
  }

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/api/auth/login`, { email, password })
      .pipe(
        tap((res: any) => {
          if (res.isSuccess) {
            this.cookieService.set('access_token', res.data.accessToken, 7);
            this.isAuthenticated.set(true);
          }
        })
      );
  }

  getToken(): string | null {
    return this.cookieService.get('access_token');
  }

  checkAuth() {
    this.isAuthenticated.set(!!this.cookieService.get('access_token'));
  }

  logout() {
    this.cookieService.delete('access_token');
    this.isAuthenticated.set(false);
    this.router.navigate(['/login']);
  }
    getRoles(): Observable<{ data: string[] }> {
    return this.http.get<{ data: string[] }>(`${this.apiUrl}/api/Auth/Roles`);
  }
}