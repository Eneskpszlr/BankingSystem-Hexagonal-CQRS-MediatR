import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { createAccountForm, toCreateAccountRequest } from '../../validations/create-account.form';

@Component({
  selector: 'app-create-account',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './create-account.html',
  styleUrl: './create-account.css',
})
export class CreateAccount {
  private _accountService = inject(AccountService);
  private _router = inject(Router);

  // 1. Formu Factory'den üretiyoruz
  form = createAccountForm();

  // Loading durumu (Çift tıklamayı önlemek için)
  isSubmitting = false;

  // 2. Kaydet Butonuna Basınca
  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched(); // Hataları kırmızı yak
      return;
    }

    this.isSubmitting = true;

    // 3. Form verisini DTO'ya çevir (Mapper fonksiyonu)
    const request = toCreateAccountRequest(this.form);

    // 4. Servise gönder
    this._accountService.create(request).subscribe({
      next: (response) => {
        console.log('Hesap oluşturuldu, ID:', response.data);
        this.isSubmitting = false;
        
        // Başarılıysa listeye geri dön
        this._router.navigate(['/accounts']); 
      },
      error: (err) => {
        console.error('Hata:', err);
        this.isSubmitting = false;
        // Buraya ileride Toastr hata mesajı eklenecek
      }
    });
  }

  // Helper: Hata mesajı göstermek için (HTML'i temiz tutar)
  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}
