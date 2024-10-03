import { Component, inject, OnInit } from '@angular/core';
import { SportsService } from '../../_services/sports.service';
import { UserActivity } from '../../_modules/useractivity';

@Component({
  selector: 'app-other-sport',
  standalone: true,
  imports: [],
  templateUrl: './other-sport.component.html',
  styleUrl: './other-sport.component.css'
})
export class OtherSportComponent implements OnInit {
  private userActivityService = inject(SportsService);
  userActivities: UserActivity[] = []; 

  ngOnInit(): void {
    this.loadSports();
  }

  loadSports() {
    this.userActivityService.getSports().subscribe({
      next: userActivities => this.userActivities = userActivities
    })
  }

}
