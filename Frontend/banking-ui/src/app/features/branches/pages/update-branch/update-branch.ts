import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { BranchService } from '../../services/branch.service';
import { updateBranchForm } from '../../validations/update-branch.form';
import { Loader } from '../../../../shared/components/loader/loader';
import { NotificationService } from '../../../../core/services/notification';

@Component({
  selector: 'app-update-branch',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './update-branch.html',
  styleUrl: './update-branch.css',
})
export class UpdateBranch implements OnInit {
  private _branchService = inject(BranchService);
  private _notificationService = inject(NotificationService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);
  private cd = inject(ChangeDetectorRef);

  form = updateBranchForm();
  
  isLoading = true;
  isSubmitting = false;
  branchId: number = 0;

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this.branchId = Number(id);
      this.loadBranch(this.branchId);
    } else {
      this._router.navigate(['/branches']);
    }
  }

  // 1. Mevcut Veriyi Getir ve Forma Doldur
  loadBranch(id: number) {
    this.isLoading = true;
    
    this._branchService.getById(id)
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (res: any) => {
          const data = res.data || res; 

          if (data) {
            this.form.patchValue({
              name: data.branchName,
              street: data.street,
              city: data.city,
              country: data.country,
              zipCode: data.zipCode
            });
          }
        },
        error: (err) => {
          console.error('Veri yüklenemedi:', err);
          this._notificationService.error('Şube bilgileri yüklenemedi.');
          this._router.navigate(['/branches']);
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

    const request = {
      id: this.branchId,
      branchName: raw.name,
      street: raw.street,
      city: raw.city,
      country: raw.country,
      zipCode: raw.zipCode
    };

    this._branchService.update(request)
      .pipe(
        finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: () => {
          this._notificationService.success('Şube başarıyla güncellendi! 🎉');
          this._router.navigate(['/branches']);
        },
        error: (err) => {
          console.error('Güncelleme hatası:', err);
        }
      });
  }
}