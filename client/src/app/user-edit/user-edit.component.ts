import { Component, HostListener, inject, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { AccountService } from '../_services/account.service';
import { MemberService } from '../_services/member.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TextInputComponent } from '../_forms/text-input/text-input.component';

@Component({
  selector: 'app-user-edit',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent],
  templateUrl: './user-edit.component.html',
  styleUrl: './user-edit.component.css'
})
export class UserEditComponent implements OnInit {
  private fb = inject(FormBuilder);
  editForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;

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
    this.initializeForm();
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

  initializeForm() {
    this.editForm = this.fb.group({
      username: [this.member?.username, Validators.required],
      email: [this.member?.email, [Validators.required, Validators.email]],
      location: [this.member?.location, Validators.required]
    });
    this.editForm.controls['password'].valueChanges.subscribe({
      next: () => this.editForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }

  updateMember() {
    console.log(this.editForm?.value);
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success('Profile updated successfully.');
        this.editForm?.reset(this.member);
      },
      error: error => this.validationErrors = error
    });
  }
}
