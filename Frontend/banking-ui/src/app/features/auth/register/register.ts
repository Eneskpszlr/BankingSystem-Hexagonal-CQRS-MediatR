import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { NotificationService } from '../../../core/services/notification';
import { CustomValidators } from '../../../shared/validators/custom-validators';
import { Loader } from '../../../shared/components/loader/loader';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './register.html',
  styleUrl: '../login/login.css'
})
export class Register {
  private _fb = inject(FormBuilder);
  private _authService = inject(AuthService);
  private _router = inject(Router);
  private _notificationService = inject(NotificationService);

  isLoading = false;

  form = this._fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    userName: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required]
  }, {
    validators: CustomValidators.match('password', 'confirmPassword')
  });

  onSubmit() {
    if (this.form.invalid) return;

    this.isLoading = true;
    const raw = this.form.getRawValue();

    const request = {
      firstName: raw.firstName!,
      lastName: raw.lastName!,
      email: raw.email!,
      userName: raw.userName!,
      password: raw.password!,
      confirmPassword: raw.confirmPassword!
    };

    this._authService.register(request).subscribe({
      next: (res) => {
        this.isLoading = false;
        this._notificationService.success('Kayıt başarılı! Giriş yapabilirsiniz.');
        this._router.navigate(['/auth/login']);
      },
      error: (err) => {
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && (control.touched || control.dirty));
  }
}