import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';
import { HasRoleDirective } from '../_directives/has-role.directive';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe, HasRoleDirective],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  private toaster = inject(ToastrService);
  private router = inject(Router);
  model: any = {};

    login() {
        console.log(this.model);
        this.accountService.login(this.model).subscribe({
            next: response => {
              console.log(response);
            }, 
            error: error => this.toaster.error(error.error)
        });
    }

    logout() {
      this.accountService.logout();
      this.router.navigate(['/']);
      this.model = {};
    }

}
