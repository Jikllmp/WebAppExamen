import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, LoginRequest, RegisterRequest } from './api/auth';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {
  private readonly TOKEN_KEY = 'authToken';
  private _token: string | null = null;
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);

  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    this._token = localStorage.getItem(this.TOKEN_KEY);
    this.isLoggedInSubject.next(!!this._token);
  }

  get token(): string | null {
    return this._token;
  }

  login(credentials: LoginRequest): void {
    this.authService.login(credentials).subscribe({
      next: response => {
        localStorage.setItem(this.TOKEN_KEY, response.token);
        this._token = response.token;
        this.isLoggedInSubject.next(true);
        this.router.navigate(['/']);
      },
      error: err => console.error(err)
    });
  }

  register(data: RegisterRequest): void {
    this.authService.register(data).subscribe({
      next: () => this.router.navigate(['/login']),
      error: err => console.error(err)
    });
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    this._token = null;
    this.isLoggedInSubject.next(false);
    this.router.navigate(['/login']);
  }

  getRole(): string | null {
    if (!this._token) return null;
    const payload = JSON.parse(atob(this._token.split('.')[1]));
    return payload.role || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  }
}