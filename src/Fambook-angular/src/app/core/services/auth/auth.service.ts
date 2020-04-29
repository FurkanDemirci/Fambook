import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { map } from 'rxjs/operators';
import { Credentials } from 'src/app/shared/models/credentials.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = environment.apiUrl;

  constructor(private http: HttpClient) { }

  login(credentials) {
    return this.http.post(this.url + '/auth/authenticate', JSON.stringify(credentials))
      .pipe(
        map(
          response => {
            let credentialsWithToken: any = response;
            if (credentialsWithToken !== null) {
              let credentials = credentialsWithToken.credentials as Credentials;
              localStorage.setItem('token', credentialsWithToken.token);
              localStorage.setItem('credentials', JSON.stringify(credentials));
              return true;
            }
          }
        )
      );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('credentials');
  }

  isLoggedIn() {
    const jwtHelper = new JwtHelperService();
    const token = localStorage.getItem('token');
    return !jwtHelper.isTokenExpired(token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  get currentUser(): Credentials {
    let parsedUser = JSON.parse(localStorage.getItem('credentials'));
    return new Credentials().deserialize(parsedUser);
  }
}
