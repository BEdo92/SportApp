import { Component,  inject,  OnInit,  output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { JsonPipe, NgIf } from '@angular/common';
import { TextInputComponent } from "../_forms/text-input/text-input.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, JsonPipe, NgIf, TextInputComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  cancelRegister = output<boolean>();
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4),
        Validators.maxLength(15)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
      email: ['', [Validators.required, Validators.email]],
      location: ['', Validators.required]
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true}; 
    }
  }

  register() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: _ => this.router.navigateByUrl('/usersport'),
      error: error => this.validationErrors = error
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }
}
