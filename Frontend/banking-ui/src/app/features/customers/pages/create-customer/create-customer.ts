import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { CustomerService } from '../../services/customer.service';
import { createCustomerForm, toCreateCustomerRequest } from '../../validations/create-customer.form';
import { Loader } from '../../../../shared/components/loader/loader';
import { NotificationService } from '../../../../core/services/notification';

@Component({
  selector: 'app-create-customer',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './create-customer.html',
  styleUrl: './create-customer.css',
})
export class CreateCustomer {
  private _customerService = inject(CustomerService);
  private _notificationService = inject(NotificationService); // Inject ettiğinden emin ol
  private _router = inject(Router);
  private cd = inject(ChangeDetectorRef);

  form = createCustomerForm();
  isSubmitting = false;
  // errorMessage değişkenini sildik, gerek yok.

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const request = toCreateCustomerRequest(this.form);

    this._customerService.create(request)
      .pipe(
        finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (res) => { // 'res' CommandResponse tipindedir
          // Backend'den gelen mesajı göster
          this._notificationService.success(res.message || 'Müşteri başarıyla oluşturuldu');
          this._router.navigate(['/customers']);
        },
        error: (err) => {
          console.error('Hata:', err);
          // BURADAKİ MANUEL HATA YÖNETİMİNİ SİLDİK.
          // Interceptor otomatik olarak Toast mesajı basacaktır.
        }
      });
  }

  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}