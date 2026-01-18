import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { CustomerService } from '../../services/customer.service';
import { updateCustomerForm } from '../../validations/update-customer.form';
import { Loader } from '../../../../shared/components/loader/loader';
import { UppercaseDirective } from '../../../../shared/directives/uppercase';
import { NotificationService } from '../../../../core/services/notification';

@Component({
  selector: 'app-update-customer',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader, UppercaseDirective],
  templateUrl: './update-customer.html',
  styleUrl: './update-customer.css',
})
export class UpdateCustomer implements OnInit {
  private _customerService = inject(CustomerService);
  private _notificationService = inject(NotificationService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);
  private cd = inject(ChangeDetectorRef);

  form = updateCustomerForm();
  
  isLoading = true;
  isSubmitting = false;
  
  // Verileri saklamak için
  customerId: number = 0;
  currentCustomer: any = null; 

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this.customerId = Number(id);
      this.loadCustomer(this.customerId);
    } else {
      this._router.navigate(['/customers']);
    }
  }

  // 1. Mevcut Veriyi Getir
  loadCustomer(id: number) {
    this.isLoading = true;

    this._customerService.getById(id)
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (res: any) => {
          console.log('Backend Müşteri Detayı:', res);

          // Veri yapısını algılar
          const data = res.data || res;
          this.currentCustomer = data;

          if (data) {
            // Formu Doldur (Mapping)
            this.form.patchValue({
              id: data.id,
              email: data.email,
              phone: data.phone,
              street: data.street || data.address?.street || data.addressLine,
              city: data.city || data.address?.city,
              country: data.country || data.address?.country,
              zipCode: data.zipCode || data.address?.zipCode
            });
          }
        },
        error: (err) => {
          console.error('Müşteri yüklenemedi:', err);
          this._notificationService.error('Müşteri bilgileri yüklenemedi.');
          this._router.navigate(['/customers']);
        }
      });
  }

  // 2. Güncelle
  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const raw = this.form.getRawValue();

    // Request Paketini Manuel Oluştur
    const request = {
      id: this.customerId,
      email: raw.email,
      phone: raw.phone,
      street: raw.street,
      city: raw.city,
      country: raw.country,
      zipCode: raw.zipCode,
      
      firstName: this.currentCustomer?.firstName,
      lastName: this.currentCustomer?.lastName,
      identityNumber: this.currentCustomer?.identityNumber
    };

    this._customerService.update(request)
      .pipe(
        finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: () => {
          // Başarı Mesajı
          this._notificationService.success('Müşteri başarıyla güncellendi! 🎉');
          this._router.navigate(['/customers']);
        },
        error: (err) => {
          console.error('Güncelleme hatası:', err);
        }
      });
  }
}