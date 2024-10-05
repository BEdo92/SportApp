import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { Member } from '../_models/member';
import { AccountService } from '../_services/account.service';
import { MemberService } from '../_services/member.service';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-edit.component.html',
  styleUrl: './user-edit.component.css'
})
export class UserEditComponent implements OnInit {
  @ViewChild('editForm') editForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }

  member?: Member;
  private accountService = inject(AccountService);
  private memberService = inject(MemberService);
  private toastr = inject(ToastrService);

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const user = this.accountService.currentUser();
    if (!user) {
      return;
    }
    this.memberService.getMember(user.username).subscribe({
      next: member => {
        this.member = member;
      }
    });
  }

  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success('Profile updated successfully.');
        this.editForm?.reset(this.member);
      },
      error: error => this.toastr.error(error.error)
    });
  }
}