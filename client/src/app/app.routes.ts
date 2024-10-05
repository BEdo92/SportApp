import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserSportComponent } from './usersport/user-sport/user-sport.component';
import { OtherSportComponent } from './othersport/other-sport/other-sport.component';
import { authGuard } from './_guards/auth.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { preventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [
            {path: 'usersport', component: UserSportComponent},
            {path: 'useredit', component: UserEditComponent, 
                canDeactivate: [preventUnsavedChangesGuard]},
            {path: 'othersport', component: OtherSportComponent},
        ]
    },
    {path: 'not-found', component: NotFoundComponent, pathMatch: 'full'},
    {path: '*server-error', component: ServerErrorComponent, pathMatch: 'full'},
    {path: '**', component: HomeComponent, pathMatch: 'full'}
];
