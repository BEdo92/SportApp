import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserActivity } from '../_modules/useractivity';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class SportsService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  baseUrl = environment.apiUrl;

  getSports() {
    return this.http.get<UserActivity[]>(this.baseUrl + 'useractivities', this.getHttpOptions());
  }

  getSport(username: string) {
    return this.http.get<UserActivity>(this.baseUrl + 'useractivities/' + username, this.getHttpOptions());
  }

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.accountService.currentUser()?.token}`
      })
    }
  }
}
