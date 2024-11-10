import { HttpClient } from '@angular/common/http';
import { inject, Injectable, model, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserActivity } from '../_models/useractivity';
import { SportSummary } from '../_models/sportsummary';
import { SportActivity } from '../_models/sportactivity';
import { PaginatedResult } from '../_models/pagination';
import { SportParams } from '../_models/sportparams';
import { setPaginatedResponse, setPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class SportsService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  memberCache = new Map();
  paginatedResult = signal<PaginatedResult<SportActivity[]> | null>(null);
  sportParams: SportParams = new SportParams();

  getSports() {
    return this.http.get<UserActivity[]>(this.baseUrl + 'useractivities');
  }

  getSportsOfUser(username: string) {
    return this.http.get<SportSummary[]>(this.baseUrl + 'useractivities/user/' + username);
  }

  getSportTypes() {
    return this.http.get<string[]>(this.baseUrl + 'sporttypes');
  }
  
  updateSportTypes(sportTypes: string[]) {
    return this.http.put<string[]>(this.baseUrl + 'sporttypes', sportTypes);
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

  filterActivities() {
    let params = setPaginationHeaders(this.sportParams.pageNumber, this.sportParams.pageSize);

    if (this.sportParams.dateFrom) {
      params = params.append('dateFrom', this.sportParams.dateFrom);
    }
    if (this.sportParams.dateTo) {
      params = params.append('dateTo', this.sportParams.dateTo);
    }
    if (this.sportParams.distanceFrom) {
      params = params.append('distanceFrom', this.sportParams.distanceFrom.toString());
    }
    if (this.sportParams.distanceTo) {
      params = params.append('distanceTo', this.sportParams.distanceTo.toString());
    }
    if (this.sportParams.sportType) {
      params = params.append('sportType', this.sportParams.sportType);
    }
    if (this.sportParams.orderBy) {
      params = params.append('orderBy', this.sportParams.orderBy);
    }

    // if (model.dateFrom) {
    //   model.dateFrom = new Date(model.dateFrom).toISOString();
    // }
    // if (model.dateTo) {
    //   model.dateTo = new Date(model.dateTo).toISOString();
    // }

    return this.http.get<SportActivity[]>(this.baseUrl + 'useractivities/user/sports/', {observe: 'response', params}).subscribe({
      next: response => {
        setPaginatedResponse(response, this.paginatedResult);
        this.memberCache.set(Object.values(this.sportParams).join('-'), response);
      }
    })
  }

  resetSportParams() {
    this.sportParams = new SportParams();
  }
}