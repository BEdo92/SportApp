import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserSportComponent } from './usersport/user-sport/user-sport.component';
import { OtherSportComponent } from './othersport/other-sport/other-sport.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'usersport', component: UserSportComponent},
    {path: 'othersport', component: OtherSportComponent},
    {path: '**', component: HomeComponent, pathMatch: 'full'}
];
