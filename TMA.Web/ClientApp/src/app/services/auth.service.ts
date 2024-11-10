import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { TokenService } from './token.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService  {

  private isLoggedInSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.hasToken());
  isLoggedIn$: Observable<boolean> = this.isLoggedInSubject.asObservable();
  constructor(private http: HttpClient, public tokenService: TokenService, private router: Router) { }

  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post('api/auth/login', credentials);
   
  }

  hasPermission(permission: string): boolean {
    const permissions = (this.tokenService.getItem('permissions') || []) as string[];
    return permissions.includes(permission);
  }


  isAdmin(): boolean {
    const userRoleId = this.tokenService.getItem('userRoleId');
    return userRoleId === '1';
  }


  logout(): void {
    this.tokenService.setAccessToken('');
    localStorage.clear();
    this.router.navigate(['/login']);
    this.updateLoginStatus(false);
  }

  isLoggedIn(): boolean {
    return !!this.tokenService.getAccessToken();
  }

  updateLoginStatus(isLoggedIn: boolean): void {
    this.isLoggedInSubject.next(isLoggedIn);
  }
  private hasToken(): boolean {
    const token = this.tokenService.getAccessToken();
    return token !== null && token !== undefined;
  }

  getUserPermissions(): string[] {
    return JSON.parse(localStorage.getItem('permissions') || '[]');
  }

}
