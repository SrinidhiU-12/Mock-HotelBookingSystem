import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
  phoneNumber: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  userId: string;
  fullName: string;
  email: string;
  role: string;
  token: string;
  expiresAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authUrl = `${environment.apiBaseUrl}/auth`;
  private readonly tokenKey = 'stayease_token';
  private readonly userKey = 'stayease_user';

  readonly currentUser = signal<AuthResponse | null>(
    this.readStoredUser()
  );

  constructor(private http: HttpClient) {}

  register(request: RegisterRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.authUrl}/register`, request)
      .pipe(tap(response => this.saveSession(response)));
  }

  login(request: LoginRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.authUrl}/login`, request)
      .pipe(tap(response => this.saveSession(response)));
  }

  getToken(): string | null {
    return this.getStorage()?.getItem(this.tokenKey) ?? null;
  }

  logout(): void {
    const storage = this.getStorage();

    storage?.removeItem(this.tokenKey);
    storage?.removeItem(this.userKey);

    this.currentUser.set(null);
  }

  private saveSession(response: AuthResponse): void {
    const storage = this.getStorage();

    storage?.setItem(this.tokenKey, response.token);
    storage?.setItem(this.userKey, JSON.stringify(response));

    this.currentUser.set(response);
  }

  private readStoredUser(): AuthResponse | null {
    const storedUser = this.getStorage()?.getItem(this.userKey);

    if (!storedUser) {
      return null;
    }

    try {
      return JSON.parse(storedUser) as AuthResponse;
    } catch {
      return null;
    }
  }

  private getStorage(): Storage | null {
    return typeof window !== 'undefined'
      ? window.sessionStorage
      : null;
  }
}