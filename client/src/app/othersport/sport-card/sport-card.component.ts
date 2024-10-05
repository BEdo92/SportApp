import { Component, input } from '@angular/core';
import { UserActivity } from '../../_models/useractivity';

@Component({
  selector: 'app-sport-card',
  standalone: true,
  imports: [],
  templateUrl: './sport-card.component.html',
  styleUrl: './sport-card.component.css'
})
export class SportCardComponent {
  activity = input.required<UserActivity>();

}
