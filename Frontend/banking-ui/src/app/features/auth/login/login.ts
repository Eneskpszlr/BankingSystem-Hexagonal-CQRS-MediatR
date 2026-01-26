import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { NotificationService } from '../../../core/services/notification';
import { Loader } from '../../../shared/components/loader/loader';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  private _fb = inject(FormBuilder);
  private _authService = inject(AuthService);
  private _router = inject(Router);
  private _notificationService = inject(NotificationService);
  private cd = inject(ChangeDetectorRef);

  isLoading = false;

  form = this._fb.group({
    userName: ['', Validators.required],
    password: ['', Validators.required]
  });

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isLoading = true;

    const request = {
      identifier: this.form.value.userName!, 
      password: this.form.value.password!
    };

    this._authService.login(request)
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: () => {
          this._notificationService.success('Giriş başarılı! Yönlendiriliyorsunuz...');
          this._router.navigate(['/']); // Dashboard'a git
        },
        error: (err) => {
          console.error('Giriş Hatası Detayı:', err);

          if (err.status === 400 && err.error?.errors) {
             const validationErrors = err.error.errors;
             
             Object.keys(validationErrors).forEach(key => {
                 let controlName = key.charAt(0).toLowerCase() + key.slice(1);
                 
                 if(controlName === 'identifier') controlName = 'userName';

                 const control = this.form.get(controlName);
                 if (control) {
                     control.setErrors({ serverError: validationErrors[key][0] });
                     control.markAsTouched();
                 }
             });
          }
          else if (err.error?.title) {
             this._notificationService.error(err.error.title);
          }
          else if (typeof err.error === 'string') {
             this._notificationService.error(err.error);
          }
          else {
             this._notificationService.error('Giriş başarısız. Bilgilerinizi kontrol edin.');
          }
        }
      });
  }

  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && (control.touched || control.dirty));
  }

  getServerError(controlName: string): string | null {
    const control = this.form.get(controlName);
    return control?.errors?.['serverError'] || null;
  }
}