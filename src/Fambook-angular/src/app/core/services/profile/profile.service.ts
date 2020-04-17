import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends ApiService {

  constructor(http: HttpClient) {
    super('/profile', http);
  }

  uploadAvatar(resource: any, id: number) {
    return this.http.post(`${this.url}/upload/${id}`, resource, {
      reportProgress: true,
      observe: 'events'
    });
  }
}
