import { Component, inject, input, OnInit } from '@angular/core';
import { SportsService } from '../../_services/sports.service';
import { SportSummary } from '../../_models/sportsummary';

@Component({
  selector: 'app-sport-card',
  standalone: true,
  imports: [],
  templateUrl: './sport-card.component.html',
  styleUrl: './sport-card.component.css'
})
export class SportCardComponent implements OnInit {
  sportType = input.required<string>();
  sportSummary: SportSummary | undefined;
  private sportService = inject(SportsService);

  ngOnInit(): void {
    this.loadSportsByTypes();
  }

  loadSportsByTypes() {
    this.sportService.getSportByType(this.sportType()).subscribe({
      next: sportSummaries => {
        this.sportSummary = sportSummaries;
        console.log(this.sportSummary);
      },
      error: error => console.log(error)
    });
  }
}
