import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { BranchService } from '../../services/branch.service';
import { createBranchForm, toCreateBranchRequest } from '../../validations/create-branch.form';
import { UppercaseDirective } from '../../../../shared/directives/uppercase';
import { Loader } from '../../../../shared/components/loader/loader';
import { NotificationService } from '../../../../core/services/notification';

@Component({
  selector: 'app-create-branch',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, UppercaseDirective, Loader],
  templateUrl: './create-branch.html',
  styleUrl: './create-branch.css',
})
export class CreateBranch {
  private _branchService = inject(BranchService);
  private _router = inject(Router);
  private cd = inject(ChangeDetectorRef);
  private _notificationService = inject(NotificationService);

  form = createBranchForm();
  isSubmitting = false;
  errorMessage = ''; // Backend hatalarını göstermek için

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = ''; // Önceki hataları temizle

    const request = toCreateBranchRequest(this.form);

    this._branchService.create(request)
      .pipe(
        finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: () => {
          this._notificationService.success('Şube başarıyla oluşturuldu!');
          this._router.navigate(['/branches']);
        },
        error: (err) => {
          console.error('Hata Detayı:', err);
          
          // 400 HATASI YAKALAMA (Validation Errors)
          if (err.status === 400) {
            if (err.error?.errors) {
              this.errorMessage = Object.values(err.error.errors).flat().join(', ');
            } else {
              this.errorMessage = err.error?.title || 'Girdiğiniz bilgiler geçersiz (400).';
            }
          } else {
            this.errorMessage = 'Bir hata oluştu. Lütfen tekrar deneyin.';
          }
        }
      });
  }

  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}