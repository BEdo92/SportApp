import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-sport',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-sport.component.html',
  styleUrl: './user-sport.component.css'
})
export class UserSportComponent {
  model: any = {};

  saveData() {

  }

  cancel() {
    //this.cancelRegister.emit(false);
    console.log('cancelled');
  }
}
