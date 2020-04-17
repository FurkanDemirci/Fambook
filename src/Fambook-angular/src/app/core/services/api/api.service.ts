import { Injectable, Inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  protected url = environment.apiUrl;

  constructor(@Inject(String) private location, protected http: HttpClient) {
    this.url = this.url + this.location;
  }

  getAll() {
    return this.http.get(this.url + '/get');
  }

  get(id: any) {
    console.log(id);
    return this.http.get(this.url + '/' + id);
  }

  create(resource: any) {
    return this.http.post(this.url + '/create', resource);
  }

  update(resource: any) {
    return this.http.put(this.url + '/update', resource);
  }

  delete(resource: any) {
    return this.http.delete(this.url + '/delete', resource);
  }
}
