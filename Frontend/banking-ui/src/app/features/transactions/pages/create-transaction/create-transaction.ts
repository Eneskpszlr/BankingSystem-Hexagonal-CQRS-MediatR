import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { TransactionService } from '../../services/transaction.service';
import { AccountService } from '../../../accounts/services/account.service';
import { Account } from '../../../../core/models/accounts';
import { createTransactionForm, toTransactionRequest } from '../../validations/transaction.form';
import { createTransferForm, toTransferRequest } from '../../validations/transfer.form';
import { Loader } from '../../../../shared/components/loader/loader';
import { NotificationService } from '../../../../core/services/notification';

type OperationType = 'Deposit' | 'Withdraw' | 'Transfer';

@Component({
  selector: 'app-create-transaction',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, Loader],
  templateUrl: './create-transaction.html',
  styleUrl: './create-transaction.css',
})
export class CreateTransaction implements OnInit {
  private _transactionService = inject(TransactionService);
  private _accountService = inject(AccountService);
  private _notificationService = inject(NotificationService);
  private _router = inject(Router);
  private cd = inject(ChangeDetectorRef);

  accounts: any[] = [];
  isLoading = false;
  isSubmitting = false;
  errorMessage = '';

  selectedOperation: OperationType = 'Deposit';

  simpleForm = createTransactionForm();
  transferForm = createTransferForm();

  ngOnInit(): void {
    this.loadAccounts();
  }

  loadAccounts() {
    this.isLoading = true;
    
    this._accountService.getAll()
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (res: any) => {
          console.log('İşlem Sayfası İçin Hesaplar:', res);
          
          if (Array.isArray(res)) {
             this.accounts = res;
          } else if (res && res.data) {
             this.accounts = res.data;
          } else {
             this.accounts = [];
          }
        },
        error: (err) => {
          console.error('Hesaplar yüklenemedi', err);
          this._notificationService.error('Hesap listesi yüklenemedi.');
        }
      });
  }

  setOperation(type: OperationType) {
    this.selectedOperation = type;
    this.errorMessage = '';
    
    this.simpleForm.reset({ currencyCode: 'TRY' });
    this.transferForm.reset({ currencyCode: 'TRY' });
  }

  onSubmit() {
    this.errorMessage = '';
    this.isSubmitting = true;

    if (this.selectedOperation === 'Transfer') {
      this.handleTransfer();
    } else {
      this.handleSimpleTransaction();
    }
  }

  private handleSimpleTransaction() {
    if (this.simpleForm.invalid) {
      this.simpleForm.markAllAsTouched();
      this.isSubmitting = false;
      return;
    }

    const request = toTransactionRequest(this.simpleForm);

    const request$ = this.selectedOperation === 'Deposit' 
      ? this._transactionService.deposit(request) 
      : this._transactionService.withdraw(request);

    request$
      .pipe(
        finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: () => this.onSuccess(),
        error: (err) => this.onError(err)
      });
  }

  private handleTransfer() {
    if (this.transferForm.invalid) {
      this.transferForm.markAllAsTouched();
      this.isSubmitting = false;
      return;
    }

    const request = toTransferRequest(this.transferForm);

    this._transactionService.transfer(request)
      .pipe(
        finalize(() => {
          this.isSubmitting = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: () => this.onSuccess(),
        error: (err) => this.onError(err)
      });
  }

  private onSuccess() {
    // Başarılı Mesajı
    const msg = this.selectedOperation === 'Transfer' 
      ? 'Transfer işlemi başarıyla gerçekleşti! 🚀' 
      : 'İşlem başarıyla tamamlandı! 💸';
      
    this._notificationService.success(msg);
    this._router.navigate(['/transactions']); 
  }

  private onError(err: any) {
    console.error('İşlem Hatası:', err);
    
    if (err.status === 400) {
      if (err.error?.errors) {
        this.errorMessage = Object.values(err.error.errors).flat().join(', ');
      } else {
        this.errorMessage = err.error?.title || 'İşlem gerçekleştirilemedi.';
      }
    } else {
      this.errorMessage = 'Beklenmedik bir hata oluştu.';
    }
  }

  hasError(controlName: string, errorName: string): boolean {
    const activeForm = this.selectedOperation === 'Transfer' ? this.transferForm : this.simpleForm;
    const control = (activeForm as FormGroup).get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}