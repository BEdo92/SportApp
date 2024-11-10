import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { SportsService } from '../../_services/sports.service';
import { DatePickerComponent } from '../../_forms/date-picker/date-picker.component';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-sport-filter',
  standalone: true,
  imports: [DatePickerComponent, CommonModule, PaginationModule, ButtonsModule, FormsModule],
  templateUrl: './user-sport-filter.component.html',
  styleUrl: './user-sport-filter.component.css'
})
export class UserSportFilterComponent implements OnInit {
  accountService = inject(AccountService);
  sportTypes: string[] = [];
  sportService = inject(SportsService);
  validationErrors: string[] | undefined;

  ngOnInit(): void {
    this.loadSportTypes();
  }

  filterData() {
    this.sportService.filterActivities();
  }

  resetFilters() {
    this.sportService.resetSportParams();
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

  pageChanged(event: any) {
    if (this.sportService.sportParams.pageNumber != event.page) {
      this.sportService.sportParams.pageNumber = event.page;
      this.filterData();
    }
  }
}
