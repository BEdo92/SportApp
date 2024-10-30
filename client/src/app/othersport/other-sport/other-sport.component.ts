import { Component, inject, OnInit } from '@angular/core';
import { SportsService } from '../../_services/sports.service';
import { UserActivity } from '../../_models/useractivity';
import { SportCardComponent } from "../sport-card/sport-card.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { DatePickerComponent } from '../../_forms/date-picker/date-picker.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-other-sport',
  standalone: true,
  imports: [ReactiveFormsModule, SportCardComponent, TextInputComponent, DatePickerComponent, CommonModule],
  templateUrl: './other-sport.component.html',
  styleUrl: './other-sport.component.css'
})
export class OtherSportComponent implements OnInit {
  private sportService = inject(SportsService);
  sportTypes: string[] = [];
  validationErrors: string[] | undefined;
  userActivities: UserActivity[] = [];
  filterActivityForm: FormGroup = new FormGroup({});
  maxDate = new Date();
  longestDistance: number = 0;

  ngOnInit(): void {
    this.loadSportTypes();
    this.loadSports();
  }

  filterBySportType($event: Event) {
    const selectedSportType = ($event.target as HTMLSelectElement).value;
    this.sportService.getLongestByType(selectedSportType).subscribe({
      next: longestDistance => {
      this.longestDistance = longestDistance;
      console.log(this.longestDistance);
      },
      error: error => this.validationErrors = error
    });
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