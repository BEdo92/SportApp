import { Component, inject, OnInit } from '@angular/core';
import { SportsService } from '../../_services/sports.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sport-type-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './sport-type-edit.component.html',
  styleUrl: './sport-type-edit.component.css'
})
export class SportTypeEditComponent implements OnInit {
  private sportService = inject(SportsService);
  sportTypes: string[] = [];
  newSport: string = '';

  ngOnInit(): void {
    this.loadSportTypes();
  }

  addItem() {
    this.sportTypes.push(this.newSport);
    this.newSport = '';
  }

  deleteItem($event: Event) {
    const selectedSportType = ($event.target as HTMLSelectElement).value;
    this.sportTypes = this.sportTypes.filter(s => s !== selectedSportType);
  }

  updateSportTypes() {
    this.sportService.updateSportTypes(this.sportTypes).subscribe({
      next: (response) => {
        console.log(response);
        this.sportTypes = response;
      },
      error: (error) => console.error("Error updating sport types:", error)
    });
  }

  loadSportTypes() {
    this.sportService.getSportTypes().subscribe({
      next: (sportTypes) => {
        this.sportTypes = sportTypes;
      },
      error: (error) => console.error("Error loading sport types:", error)
    });
  }
}
