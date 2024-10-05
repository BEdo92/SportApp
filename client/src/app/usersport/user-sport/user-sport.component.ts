import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SportsService } from '../../_services/sports.service';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-user-sport',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-sport.component.html',
  styleUrl: './user-sport.component.css'
})
export class UserSportComponent implements OnInit {
  model: any = {};
  accountService = inject(AccountService);
  sportTypes: string[] = [];
  private sportService = inject(SportsService);
  private toastr = inject(ToastrService);
  private cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.loadSportTypes();
  }

  saveData() {
    this.sportService.saveActivity(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error => this.toastr.error(error.error)
    });
  }

  cancel() {
    //this.cancelRegister.emit(false);
    console.log('cancelled');
  }

  loadSportTypes() {
    this.sportService.getSportTypes().subscribe({
      next: sportTypes => {
        this.sportTypes = sportTypes;
        this.cdr.detectChanges();
      },
      error: error => this.toastr.error(error.error)
    });
  }
}
