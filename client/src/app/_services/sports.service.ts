import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserActivity } from '../_models/useractivity';
import { SportSummary } from '../_models/sportsummary';
import { SportActivity } from '../_models/sportactivity';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class SportsService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResult = signal<PaginatedResult<SportActivity[]> | null>(null);

  getSports() {
    return this.http.get<UserActivity[]>(this.baseUrl + 'useractivities');
  }

  getSportsOfUser(username: string) {
    return this.http.get<SportSummary[]>(this.baseUrl + 'useractivities/user/' + username);
  }

  getSportTypes() {
    return this.http.get<string[]>(this.baseUrl + 'sporttypes');
  }

  saveActivity(model: any) {
    return this.http.post(this.baseUrl + 'useractivities', model);
  }

  getSportByType(sportType: string) {
    return this.http.get<SportSummary>(this.baseUrl + 'useractivities/sport/' + sportType);
  }

  getLongestByType(sportType: string) {
    return this.http.get<number>(this.baseUrl + 'useractivities/longest/' + sportType);
  }

  filterActivity(model: any) {
    console.log(model);
    console.log(this.baseUrl + 'useractivities/user/sports/', {observe: 'response', params: model});
    return this.http.get<SportActivity[]>(this.baseUrl + 'useractivities/user/sports/', {observe: 'response', params: model}).subscribe({
      next: response => {
        this.paginatedResult.set({
          items: response.body as SportActivity[],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        })
      }
    });
  }
}