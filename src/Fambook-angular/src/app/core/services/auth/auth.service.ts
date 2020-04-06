import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { map } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = environment.apiUrl;

  constructor(private http: HttpClient) { }

  login(credentials) {
    return this.http.post(this.url + '/user/authenticate', JSON.stringify(credentials))
      .pipe(
        map(
          response => {
            let userWithToken: any = response;
            if (userWithToken !== null) {
              let user = userWithToken.user as User;
              localStorage.setItem('token', userWithToken.token);
              localStorage.setItem('user', JSON.stringify(user));
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

  getUser() {
    return new User().deserialize(localStorage.getItem('user'));
  }
}
