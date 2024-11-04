import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { AccountService } from '../_services/account.service';
import { FormsModule } from '@angular/forms';
import { TitleCasePipe } from '@angular/common';
import { SportsService } from '../_services/sports.service';
import { SportCardComponent } from '../othersport/sport-card/sport-card.component';
import { SportSummary } from '../_models/sportsummary';
import { UserSportFilterComponent } from './user-sport-filter/user-sport-filter.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent, FormsModule, TitleCasePipe, SportCardComponent, UserSportFilterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  private sportService = inject(SportsService);
  sportsSummaries: SportSummary[] = [];
  accountService = inject(AccountService);
  registerMode = false;
  filterMode = false;
  users: any;

  ngOnInit(): void {
    this.getSportsOfUser();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  filterToggle() {
    this.filterMode = !this.filterMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  getSportsOfUser() {
    const username = this.accountService.currentUser()?.username;
    if (!username) {
        console.error('Username is undefined');
        return;
    }
    
    this.sportService.getSportsOfUser(username).subscribe({
      next: sportsSummary => {
        this.sportsSummaries = sportsSummary;
        console.log(this.sportsSummaries);
      },
      error: error => console.log(error)
    });
  }
}
