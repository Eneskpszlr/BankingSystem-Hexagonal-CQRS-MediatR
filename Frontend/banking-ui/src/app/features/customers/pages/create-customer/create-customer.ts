import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { createCustomerForm, toCreateCustomerRequest } from '../../validations/create-customer.form';

@Component({
  selector: 'app-create-customer',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './create-customer.html',
  styleUrl: './create-customer.css',
})
export class CreateCustomer {
  private _customerService = inject(CustomerService);
  private _router = inject(Router);

  form = createCustomerForm();
  isSubmitting = false;

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const request = toCreateCustomerRequest(this.form);

    this._customerService.create(request).subscribe({
      next: () => {
        console.log('Müşteri oluşturuldu');
        this.isSubmitting = false;
        this._router.navigate(['/customers']);
      },
      error: (err) => {
        console.error('Hata:', err);
        this.isSubmitting = false;
      }
    });
  }

  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}
