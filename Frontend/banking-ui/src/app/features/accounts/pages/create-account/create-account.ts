import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';

import { AccountService } from '../../services/account.service';
import { createAccountForm, toCreateAccountRequest } from '../../validations/create-account.form';
import { Loader } from '../../../../shared/components/loader/loader';
import { NotificationService } from '../../../../core/services/notification';

@Component({
  selector: 'app-create-account',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './create-account.html',
  styleUrl: './create-account.css',
})
export class CreateAccount {
  private _accountService = inject(AccountService);
  private _notificationService = inject(NotificationService);
  private _router = inject(Router);
  private cd = inject(ChangeDetectorRef);

  form = createAccountForm();
  isSubmitting = false;

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const request = toCreateAccountRequest(this.form);

    this._accountService.create(request)
      .pipe(
        finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          // Başarı Mesajı
          this._notificationService.success('Hesap başarıyla oluşturuldu! 💸');
          this._router.navigate(['/accounts']); 
        },
        error: (err) => {
          console.error('Hata:', err);
          
          if (err.status === 400 && err.error?.errors) {
             const msg = Object.values(err.error.errors).flat().join(', ');
             this._notificationService.error(msg);
          }
        }
      });
  }

  // Helper fonksiyon
  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}