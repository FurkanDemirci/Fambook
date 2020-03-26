import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = environment.apiUrl;

  constructor(private http: HttpClient) { }

  login(credentials) {
    return this.http.post(this.url + '/login', JSON.stringify(credentials))
      .pipe(
        map(
          response => {
            const token: string = response.toString();
            if (token) {
              localStorage.setItem('token', token);
              return true;
            }
          }
        )
      );
  }

  logout() {
    localStorage.removeItem('token');
  }

  isLoggedIn() {
    const jwtHelper = new JwtHelperService();
    const token = localStorage.getItem('token');
    return !jwtHelper.isTokenExpired(token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  get currentUser(): any {
    const token = localStorage.getItem('token');
    if (!token) {
      return null;
    }
    return new JwtHelperService().decodeToken(token);
  }
}