import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { updateCustomerForm, toUpdateCustomerRequest } from '../../validations/update-customer.form';
import { Loader } from '../../../../shared/components/loader/loader';

@Component({
  selector: 'app-update-customer',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './update-customer.html',
  styleUrl: './update-customer.css',
})
export class UpdateCustomer implements OnInit {
  private _customerService = inject(CustomerService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);

  form = updateCustomerForm();
  
  isLoading = true;
  isSubmitting = false;
  currentCustomer: any = null; // Read-only veriler için (Ad, Soyad, TC)

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this.loadCustomer(Number(id));
    }
  }

  loadCustomer(id: number) {
    this._customerService.getById(id).subscribe({
      next: (res) => {
        this.currentCustomer = res.data;
        
        // Formu doldur
        this.form.patchValue({
          id: res.data.id,
          email: res.data.email,
          phone: res.data.phone,
          street: res.data.street,
          city: res.data.city,
          country: res.data.country,
          zipCode: res.data.zipCode
        });
        
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isLoading = false;
        this._router.navigate(['/customers']);
      }
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const request = toUpdateCustomerRequest(this.form);

    this._customerService.update(request).subscribe({
      next: () => {
        this.isSubmitting = false;
        this._router.navigate(['/customers']);
      },
      error: (err) => {
        console.error(err);
        this.isSubmitting = false;
      }
    });
  }
}
