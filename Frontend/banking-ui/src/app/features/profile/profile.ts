import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { CustomerService } from '../customers/services/customer.service';
import { NotificationService } from '../../core/services/notification';
import { Loader } from '../../shared/components/loader/loader';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, Loader],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile implements OnInit {
  private _authService = inject(AuthService);
  private _customerService = inject(CustomerService);
  private _fb = inject(FormBuilder);
  private _notification = inject(NotificationService);

  user = this._authService.currentUser;
  isLoading = false;

  // Form (Sadece düzenlenebilir alanlar)
  form = this._fb.group({
    email: ['', [Validators.required, Validators.email]],
    phone: ['', Validators.required],
    street: ['', Validators.required],
    city: ['', Validators.required],
    zipCode: ['']
  });

  customerData: any = null;

  ngOnInit() {
    this.loadProfile();
  }

  loadProfile() {
    const userId = this.user()?.id;
    if (!userId) return;

    this.isLoading = true;
    this._customerService.getById(userId)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (data: any) => {
          this.customerData = data.data || data; 
          
          if (this.customerData) {
            this.form.patchValue({
              email: this.customerData.email,
              phone: this.customerData.phone,
              street: this.customerData.street || this.customerData.address?.street,
              city: this.customerData.city || this.customerData.address?.city,
              zipCode: this.customerData.zipCode || this.customerData.address?.zipCode
            });
          }
        },
        error: () => this._notification.error('Profil bilgileri yüklenemedi.')
      });
  }

  onSubmit() {
    if (this.form.invalid) return;

    this.isLoading = true;
    const userId = this.user()?.id!;
    
    // Backend'e gidecek update objesi
    const updateRequest = {
      id: userId,
      ...this.form.value,
      firstName: this.customerData.firstName,
      lastName: this.customerData.lastName,
      identityNumber: this.customerData.identityNumber,
      country: this.customerData.country
    };

    this._customerService.update(updateRequest as any)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this._notification.success('Profiliniz güncellendi! ✅');
        },
        error: () => this._notification.error('Güncelleme başarısız.')
      });
  }
}