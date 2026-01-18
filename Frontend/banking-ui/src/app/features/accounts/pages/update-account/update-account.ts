import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { Loader } from '../../../../shared/components/loader/loader';
import { NotificationService } from '../../../../core/services/notification';

@Component({
  selector: 'app-update-account',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './update-account.html',
  styleUrl: './update-account.css',
})
export class UpdateAccount implements OnInit {
  private _accountService = inject(AccountService);
  private _notificationService = inject(NotificationService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);
  private _fb = inject(FormBuilder);
  private cd = inject(ChangeDetectorRef);

  form = this._fb.group({
    id: [0, Validators.required],
    status: ['1', Validators.required] 
  });

  accountId: number = 0;
  isLoading = true;      
  isSubmitting = false;  
  currentAccountInfo: any = null; 

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this.accountId = Number(id);
      this.loadAccountData(this.accountId);
    } else {
      this._router.navigate(['/accounts']);
    }
  }

  loadAccountData(id: number) {
    this.isLoading = true;

    this._accountService.getById(id)
      .pipe(finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
      }))
      .subscribe({
        next: (response: any) => {
          const data = response.data || response;
          this.currentAccountInfo = data;

          if (data) {
            let statusValue = '1';

            if (data.status === 'Deleted' || data.status === 'Passive' || data.status === '3' || data.status === 3) {
              statusValue = '3';
            } 
            else {
              statusValue = '1';
            }

            this.form.patchValue({
              id: data.id,
              status: statusValue
            });
          }
        },
        error: (err) => {
          console.error(err);
          this._notificationService.error('Hesap bilgileri yüklenemedi.');
          this._router.navigate(['/accounts']);
        }
      });
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const raw = this.form.getRawValue();

    const request = {
      id: this.accountId,
      status: Number(raw.status),
      accountNumber: this.currentAccountInfo?.accountNumber,
      customerId: this.currentAccountInfo?.customerId,
      branchId: this.currentAccountInfo?.branchId,
      currencyCode: this.currentAccountInfo?.currencyCode
    };

    console.log('Backend\'e giden paket:', request);

    this._accountService.update(request as any)
      .pipe(finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
      }))
      .subscribe({
        next: () => {
          this._notificationService.success('Hesap durumu güncellendi! 🎉');
          this._router.navigate(['/accounts']);
        },
        error: (err) => {
          console.error('Güncelleme hatası', err);
        }
      });
  }
}