import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth/auth.service';
import { ServiceError } from 'src/app/shared/services/service.error';
import { ConnectionError } from 'src/app/shared/services/connection.error';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(public auth: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Links to ignore the token
        if (request.url !== `${environment.apiUrl + '/user/authenticate'}`) {
            if (request.url !== `${environment.apiUrl + '/user/create'}`) {
                request = request.clone({
                    setHeaders: {
                        'Authorization': this.auth.getToken(),
                    }
                });
            }
        }
        request = request.clone({
            setHeaders: {
                'Content-Type': 'application/json'
            }
        });

        // Console logging request for development purposes
        console.log(request);

        return next.handle(request).pipe(
            catchError(this.handleError));
    }

    protected handleError(error: Response) {
        if (error.status === 400) {
            return throwError(new ServiceError(error));
        }
        return throwError(new ConnectionError());
    }
}
