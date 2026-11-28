import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
  AbstractControl,
  ValidationErrors
} from '@angular/forms';

import { Router, RouterLink } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  showPassword = false;
  showConfirmPassword = false;
  isSubmitting = false;

  registerForm;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.nonNullable.group({
      fullName: ['', [
        Validators.required,
        Validators.minLength(3)
      ]],

      email: ['', [
        Validators.required,
        Validators.email
      ]],

      phoneNumber: ['', [
        Validators.required,
        Validators.pattern(/^[0-9]{10}$/)
      ]],

      password: ['', [
        Validators.required,
        Validators.minLength(8)
      ]],

      confirmPassword: ['', [
        Validators.required
      ]]
    }, {
      validators: this.passwordMatchValidator
    });
  }

  passwordMatchValidator(
    control: AbstractControl
  ): ValidationErrors | null {

    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (!confirmPassword) {
      return null;
    }

    return password === confirmPassword
      ? null
      : { passwordMismatch: true };
  }

  onRegister(): void {

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const {
      fullName,
      email,
      password,
      phoneNumber
    } = this.registerForm.getRawValue();

    // Send only the fields expected by the backend DTO.
    const request = {
      fullName,
      email,
      password,
      phoneNumber
    };

    this.isSubmitting = true;

    this.authService.register(request).subscribe({

      next: (response) => {
        this.isSubmitting = false;

        alert(
          `Welcome ${response.fullName}! Your account was created successfully.`
        );

        // Registration returns a JWT, which AuthService stores.
        this.router.navigate(['/customer/hotels']);
      },

      error: (error: HttpErrorResponse) => {
        this.isSubmitting = false;

        const message =
          error.error?.message ||
          'Registration failed. Please try again.';

        alert(message);
      }

    });
  }
}