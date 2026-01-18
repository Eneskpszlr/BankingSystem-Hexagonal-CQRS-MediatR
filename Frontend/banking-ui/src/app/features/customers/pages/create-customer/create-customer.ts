import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { CustomerService } from '../../services/customer.service';
import { createCustomerForm, toCreateCustomerRequest } from '../../validations/create-customer.form';
import { Loader } from '../../../../shared/components/loader/loader';

@Component({
  selector: 'app-create-customer',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './create-customer.html',
  styleUrl: './create-customer.css',
})
export class CreateCustomer {
  private _customerService = inject(CustomerService);
  private _router = inject(Router);
  private cd = inject(ChangeDetectorRef);

  form = createCustomerForm();
  isSubmitting = false;
  errorMessage = '';

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';
    const request = toCreateCustomerRequest(this.form);

    this._customerService.create(request)
      .pipe(
        finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: () => {
          console.log('Müşteri oluşturuldu');
          this._router.navigate(['/customers']);
        },
        error: (err) => {
          console.error('Hata:', err);
          
          // 400 Hata Yönetimi
          if (err.status === 400) {
             if (err.error?.errors) {
               this.errorMessage = Object.values(err.error.errors).flat().join(', ');
             } else {
               this.errorMessage = err.error?.title || 'Geçersiz müşteri bilgileri.';
             }
          } else {
             this.errorMessage = 'Müşteri oluşturulurken bir hata meydana geldi.';
          }
        }
      });
  }

  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}