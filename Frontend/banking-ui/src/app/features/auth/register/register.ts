import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { NotificationService } from '../../../core/services/notification';
import { CustomValidators } from '../../../shared/validators/custom-validators';
import { Loader } from '../../../shared/components/loader/loader';
import { finalize } from 'rxjs';

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
  private cd = inject(ChangeDetectorRef);

  isLoading = false;

  form = this._fb.group({
    tckn: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    birthDate: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    userName: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required]
  }, {
    validators: CustomValidators.match('password', 'confirmPassword')
  });

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    
    this.isLoading = true;
    const raw = this.form.getRawValue();

    const request = {
      tckn: raw.tckn!,
      firstName: raw.firstName!,
      lastName: raw.lastName!,
      birthDate: raw.birthDate!,
      email: raw.email!,
      userName: raw.userName!,
      password: raw.password!,
      confirmPassword: raw.confirmPassword!
    };

    this._authService.register(request)
    .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
    .subscribe({
      next: (res) => {
        this._notificationService.success('Kayıt başarılı! Giriş yapabilirsiniz.');
        this._router.navigate(['/auth/login']);
      },
      error: (err) => {
        console.log('🔴 Backend Ham Hata:', err);

        if (err.status === 400 && err.error?.errors) {
          const validationErrors = err.error.errors;

          Object.keys(validationErrors).forEach(key => {
            console.log(`Backend Anahtarı: ${key}`); 
            console.log(`Hata Mesajı: ${validationErrors[key]}`);

            const controlName = key.charAt(0).toLowerCase() + key.slice(1);
            console.log(`Eşleştirilen Form Alanı: ${controlName}`);

            const control = this.form.get(controlName);

            if (control) {
              console.log(`✅ ${controlName} alanı bulundu, hata basılıyor...`);
              
              control.setErrors({ 
                serverError: validationErrors[key][0] 
              });
              control.markAsTouched(); 
            } else {
              console.warn(`⚠️ Formda "${controlName}" adında bir alan bulunamadı!`);
            }
          });

          this._notificationService.error('Lütfen formdaki kırmızı hataları düzeltiniz.');
        } 
        else {
          this._notificationService.error(err.error?.title || 'Kayıt sırasında bir hata oluştu.');
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