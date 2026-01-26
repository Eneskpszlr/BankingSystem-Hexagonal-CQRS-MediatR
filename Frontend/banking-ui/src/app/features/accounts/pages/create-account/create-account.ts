import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { BranchService } from '../../../branches/services/branch.service';
import { AuthService } from '../../../../core/services/auth.service';
import { NotificationService } from '../../../../core/services/notification';
import { Loader } from '../../../../shared/components/loader/loader';

@Component({
  selector: 'app-create-account',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './create-account.html',
  styleUrl: './create-account.css'
})
export class CreateAccount implements OnInit {
  private _fb = inject(FormBuilder);
  private _accountService = inject(AccountService);
  private _branchService = inject(BranchService);
  private _authService = inject(AuthService);
  private _router = inject(Router);
  private _notificationService = inject(NotificationService);
  private cd = inject(ChangeDetectorRef);

  isLoading = false;
  branches: any[] = [];

  form = this._fb.group({
    branchId: ['', Validators.required],
    currencyCode: ['TRY', Validators.required],
    initialBalance: [0, [Validators.required, Validators.min(0)]]
  });

  ngOnInit(): void {
    this.loadBranches();
  }

  loadBranches() {
    this.isLoading = true;
    this._branchService.getAll().subscribe({
      next: (res: any) => {
        this.branches = Array.isArray(res) ? res : (res.data || []);
        if (this.branches.length > 0) {
          this.form.patchValue({ branchId: this.branches[0].id });
        }
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        this._notificationService.error('Şubeler yüklenemedi.');
      }
    });
  }

  generateAccountNumber(): string {
    // Örn: "1059384756" gibi string üretir
    return Math.floor(1000000000 + Math.random() * 9000000000).toString();
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    const raw = this.form.getRawValue();
    const currentUser = this._authService.currentUser();

    if (!currentUser) {
        this._notificationService.error('Oturum süreniz dolmuş.');
        this._router.navigate(['/auth/login']);
        return;
    }

    const request = {
      accountNumber: this.generateAccountNumber(),
      customerId: currentUser.id,
      branchId: Number(raw.branchId),
      currencyCode: raw.currencyCode,
      initialBalance: raw.initialBalance || 0
    };

    console.log('Backend Giden Veri:', request);

    this._accountService.create(request as any)
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: () => {
          this._notificationService.success('Hesap başarıyla oluşturuldu! 🎉');
          this._router.navigate(['/']);
        },
        error: (err) => {
          console.error(err);
          if (err.status === 400 && err.error?.title) {
             this._notificationService.error(err.error.title);
          } else {
             this._notificationService.error('Hesap oluşturulamadı. Lütfen bilgileri kontrol edin.');
          }
        }
      });
  }

  hasError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.hasError(errorName) && (control.touched || control.dirty));
  }
}