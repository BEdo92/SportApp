import { Component, inject } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SportsService } from '../../_services/sports.service';
import { SportActivity } from '../../_models/sportactivity';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { DatePickerComponent } from '../../_forms/date-picker/date-picker.component';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

@Component({
  selector: 'app-user-sport-filter',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, DatePickerComponent, CommonModule, PaginationModule, ButtonsModule],
  templateUrl: './user-sport-filter.component.html',
  styleUrl: './user-sport-filter.component.css'
})
export class UserSportFilterComponent {
  accountService = inject(AccountService);
  sportTypes: string[] = [];
  private fb = inject(FormBuilder);
  sportService = inject(SportsService);
  filterActivityForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  maxDate = new Date();
  filteredData: SportActivity[] = [];
  pageNumber = 1;
  pageSize = 10;

  ngOnInit(): void {
    this.loadSportTypes();
  }

  initializeForm() {
    this.filterActivityForm = this.fb.group({
      dateFrom: ['', Validators.max(this.maxDate.getTime() - 1)],
      dateTo: ['', Validators.max(this.maxDate.getTime())],
      sportType: [null],
      distanceFrom: ['', [Validators.pattern('^[0-9]*$')]],
      distanceTo: ['', [Validators.pattern('^[0-9]*$')]],
      orderByDate: ['date'],
      orderByDistance: ['distance'],
    });
  }

  filterData(orderBy: string = 'date') {
    const formValue = this.filterActivityForm.value;
    if (!formValue.sportType) {
      formValue.sportType = 'all';
    }
    formValue.orderBy = formValue.orderByDate === 'date' ? formValue.orderByDate : formValue.orderByDistance;
    formValue.pageNumber = this.pageNumber;
    formValue.pageSize = this.pageSize;
    console.log(formValue);
    this.sportService.filterActivity(formValue);
  }

  loadSportTypes() {
    this.sportService.getSportTypes().subscribe({
      next: sportTypes => {
        this.sportTypes = sportTypes;
        console.log(this.sportTypes);
        this.initializeForm();
      },
      error: error => this.validationErrors = error
    });
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.filterData();
    }
  }
}
