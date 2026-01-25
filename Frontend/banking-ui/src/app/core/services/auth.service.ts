import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { LoginRequest, RegisterRequest, AuthResponse, UserToken, LoggedInUser } from '../models/auth';
import { CommandResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _http = inject(HttpClient);
  private _router = inject(Router);

  // --- STATE (Sinyaller) ---
  // Kullanıcı bilgisini tutan sinyal
  currentUser = signal<LoggedInUser | null>(null);

  // Kullanıcı giriş yapmış mı? (Computed Signal)
  isLoggedIn = computed(() => !!this.currentUser());

  // Admin mi?
  isAdmin = computed(() => this.currentUser()?.isAdmin || false);

  private readonly TOKEN_KEY = 'onion_bank_token';

  constructor() {
    // Uygulama ilk açıldığında (F5 yapıldığında) token varsa user'ı yükle
    this.loadUserFromStorage();
  }

  // --- API İŞLEMLERİ ---

  login(request: LoginRequest): Observable<AuthResponse> {
    return this._http.post<AuthResponse>(API_ENDPOINTS.AUTH.LOGIN, request)
      .pipe(
        tap(response => {
          if (response.token) {
            this.setToken(response.token);
          }
        })
      );
  }

  register(request: RegisterRequest): Observable<CommandResponse> {
    return this._http.post<CommandResponse>(API_ENDPOINTS.AUTH.REGISTER, request);
  }

  logout() {
    localStorage.removeItem(this.TOKEN_KEY);
    this.currentUser.set(null);
    this._router.navigate(['/auth/login']);
  }

  // --- TOKEN YÖNETİMİ ---

  private setToken(token: string) {
    localStorage.setItem(this.TOKEN_KEY, token);
    this.decodeAndSetUser(token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  private loadUserFromStorage() {
    const token = this.getToken();
    if (token && !this.isTokenExpired(token)) {
      this.decodeAndSetUser(token);
    } else {
      this.logout(); // Süresi dolmuşsa çıkış yap
    }
  }

  private decodeAndSetUser(token: string) {
    try {
      const decoded: any = jwtDecode(token);
      
      const name = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || decoded['name'];
      const roleClaim = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || decoded['role'];
      const userId = decoded['sub'] || decoded['id'];
      
      const roles: string[] = Array.isArray(roleClaim) ? roleClaim : [roleClaim];

      const user: LoggedInUser = {
        id: Number(userId),
        fullName: name,
        roles: roles,
        isAdmin: roles.includes('Admin')
      };

      this.currentUser.set(user);
      
    } catch (error) {
      console.error('Token decode hatası', error);
      this.logout();
    }
  }

  private isTokenExpired(token: string): boolean {
    try {
      const decoded: any = jwtDecode(token);
      const currentTime = Math.floor(Date.now() / 1000);
      return decoded.exp < currentTime;
    } catch {
      return true;
    }
  }
}