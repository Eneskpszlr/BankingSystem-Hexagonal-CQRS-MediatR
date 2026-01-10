import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { TransactionService } from '../../services/transaction.service';
import { AccountService } from '../../../accounts/services/account.service';
import { Account } from '../../../../core/models/accounts';
import { createTransactionForm, toTransactionRequest } from '../../validations/transaction.form';
import { createTransferForm, toTransferRequest } from '../../validations/transfer.form';
import { Loader } from '../../../../shared/components/loader/loader';

// İşlem Tipleri için Tip Tanımı
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
  private _router = inject(Router);

  accounts: Account[] = [];
  isLoading = false;
  isSubmitting = false;

  // Seçili İşlem Tipi (Varsayılan: Para Yatırma)
  selectedOperation: OperationType = 'Deposit';

  // İki Farklı Form Örneği (Factory Pattern)
  // 1. Yatırma ve Çekme için Form
  simpleForm = createTransactionForm();
  
  // 2. Transfer için Form
  transferForm = createTransferForm();

  ngOnInit(): void {
    this.loadAccounts();
  }

  loadAccounts() {
    this.isLoading = true;
    this._accountService.getAll().subscribe({
      next: (res) => {
        this.accounts = res.data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Hesaplar yüklenemedi', err);
        this.isLoading = false;
      }
    });
  }

  // İşlem Tipi Değiştiğinde
  setOperation(type: OperationType) {
    this.selectedOperation = type;
    
    // Formları sıfırla
    this.simpleForm.reset({ currencyCode: 'TRY' });
    this.transferForm.reset({ currencyCode: 'TRY' });
  }

  // Form Gönderme
  onSubmit() {
    this.isSubmitting = true;

    if (this.selectedOperation === 'Transfer') {
      this.handleTransfer();
    } else {
      this.handleSimpleTransaction();
    }
  }

  // A) Para Yatırma ve Çekme İşlemi
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

    request$.subscribe({
      next: () => this.onSuccess(),
      error: (err) => this.onError(err)
    });
  }

  // B) Havale İşlemi
  private handleTransfer() {
    if (this.transferForm.invalid) {
      this.transferForm.markAllAsTouched();
      this.isSubmitting = false;
      return;
    }

    const request = toTransferRequest(this.transferForm);

    this._transactionService.transfer(request).subscribe({
      next: () => this.onSuccess(),
      error: (err) => this.onError(err)
    });
  }

  private onSuccess() {
    console.log('İşlem Başarılı');
    this.isSubmitting = false;
    this._router.navigate(['/transactions']); // Geçmiş sayfasına yönlendir
  }

  private onError(err: any) {
    console.error('İşlem Hatası:', err);
    this.isSubmitting = false;
    // Toastr eklenecek
  }

  // Helper: Hata kontrolü (Hangi formun aktif olduğuna bakar)
  hasError(controlName: string, errorName: string): boolean {
    const activeForm = this.selectedOperation === 'Transfer' ? this.transferForm : this.simpleForm;
    const control = (activeForm as FormGroup).get(controlName);
    return !!(control && control.hasError(errorName) && control.touched);
  }
}
