import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SportsService } from '../../_services/sports.service';
import { AccountService } from '../../_services/account.service';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { DatePickerComponent } from "../../_forms/date-picker/date-picker.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-sport',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, DatePickerComponent, CommonModule],
  templateUrl: './user-sport.component.html',
  styleUrl: './user-sport.component.css'
})
export class UserSportComponent implements OnInit {
  accountService = inject(AccountService);
  sportTypes: string[] = [];
  private fb = inject(FormBuilder);
  private sportService = inject(SportsService);
  saveActivityForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  maxDate = new Date();

  ngOnInit(): void {
    this.loadSportTypes();
  }

  initializeForm() {
    this.saveActivityForm = this.fb.group({
      date: ['', Validators.required, Validators.max(this.maxDate.getTime())],
      sportType: [this.sportTypes, Validators.required],
      distance: ['', [Validators.required, Validators.pattern('^[0-9]*$')]]
    });
  }

  saveData() {
    console.log(this.saveActivityForm.value);
    this.sportService.saveActivity(this.saveActivityForm.value).subscribe({
      next: response => { 
        console.log(response);
      },
      error: error => {
        console.log(error);
        this.validationErrors = error
      }
    });
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
}
