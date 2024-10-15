import { Component, inject, OnInit } from '@angular/core';
import { SportsService } from '../../_services/sports.service';
import { UserActivity } from '../../_models/useractivity';
import { SportCardComponent } from "../sport-card/sport-card.component";

@Component({
  selector: 'app-other-sport',
  standalone: true,
  imports: [SportCardComponent],
  templateUrl: './other-sport.component.html',
  styleUrl: './other-sport.component.css'
})
export class OtherSportComponent implements OnInit {
  private sportService = inject(SportsService);
  sportTypes: string[] = [];
  validationErrors: string[] | undefined;
  userActivities: UserActivity[] = []; 

  ngOnInit(): void {
    this.loadSportTypes();
    this.loadSports();
  }

  loadSports() {
    this.sportService.getSports().subscribe({
      next: userActivities => this.userActivities = userActivities
    })
  }

  loadSportTypes() {
    this.sportService.getSportTypes().subscribe({
      next: sportTypes => {
        this.sportTypes = sportTypes;
        console.log(this.sportTypes);
      },
      error: error => this.validationErrors = error
    });
  }
}