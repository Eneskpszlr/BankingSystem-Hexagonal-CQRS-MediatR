import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';
import { TransactionService } from '../../services/transaction.service';
import { AccountService } from '../../../accounts/services/account.service';
import { Transaction } from '../../../../core/models/transactions';
import { Account } from '../../../../core/models/accounts'; //
import { Loader } from '../../../../shared/components/loader/loader';
import { StatusTrPipe } from '../../../../shared/pipes/status-tr-pipe';

@Component({
  selector: 'app-transaction-history',
  standalone: true,
  imports: [CommonModule, FormsModule, Loader, StatusTrPipe],
  templateUrl: './transaction-history.html',
  styleUrl: './transaction-history.css',
})
export class TransactionHistory implements OnInit {
  private _transactionService = inject(TransactionService);
  private _accountService = inject(AccountService);
  private cd = inject(ChangeDetectorRef);

  // State
  accounts: any[] = [];
  transactions: Transaction[] = [];
  
  selectedAccountId: number = 0;
  isLoading: boolean = false;
  
  // Tabloda göstermek için seçili hesabın para birimi
  currentCurrency: string = 'TRY'; 

  ngOnInit(): void {
    this.loadAccounts();
  }

  // 1. Hesapları Getir (Sayfa Açılışı)
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
          console.log('Dropdown İçin Gelen Hesaplar:', res);

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
        }
      });
  }

  // 2. Dropdown Değişince
  onAccountChange() {
    const accId = Number(this.selectedAccountId);

    if (accId > 0) {
      // Seçilen hesabın para birimini bul (Tablo için)
      const selectedAccount = this.accounts.find(acc => acc.id === accId);
      
      this.currentCurrency = selectedAccount?.currencyCode || 'TRY';

      // İşlemleri getir
      this.loadTransactions(accId);
    } else {
      // "Seçiniz"e dönerse tabloyu temizle
      this.transactions = [];
      this.currentCurrency = 'TRY';
    }
  }

  // 3. İşlemleri Getir
  loadTransactions(accountId: number) {
    this.isLoading = true;

    this._transactionService.getByAccountId(accountId)
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.cd.detectChanges();
        })
      )
      .subscribe({
        next: (res: any) => {
          const data = res.data || res;
          this.transactions = Array.isArray(data) ? data : [];
        },
        error: (err) => {
          console.error('İşlemler yüklenemedi', err);
          this.transactions = [];
        }
      });
  }
}