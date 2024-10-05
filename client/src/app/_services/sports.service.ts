import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserActivity } from '../_models/useractivity';

@Injectable({
  providedIn: 'root'
})
export class SportsService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getSports() {
    return this.http.get<UserActivity[]>(this.baseUrl + 'useractivities');
  }

  getSport(username: string) {
    return this.http.get<UserActivity>(this.baseUrl + 'useractivities/' + username);
  }

  getSportTypes() {
    return this.http.get<string[]>(this.baseUrl + 'sporttypes');
  }

  saveActivity(model: any) {
    return this.http.post(this.baseUrl + 'useractivities', model);
  }
}
