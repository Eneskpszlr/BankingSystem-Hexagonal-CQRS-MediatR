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

  startDate: string | null = null;
  endDate: string | null = null;

  ngOnInit(): void {
    this.loadAccounts();
  }

  applyFilters() {
    if (!this.selectedAccountId || this.selectedAccountId === 0) {
      this.transactions = [];
      return;
    }

    this.isLoading = true;
    
    this.cd.detectChanges();

    const filters: any = {
      accountId: this.selectedAccountId
    };

    if (this.startDate) filters.startDate = this.startDate;
    if (this.endDate) filters.endDate = this.endDate;

    this._transactionService.getAll(filters)
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
          console.error('Filtreleme hatası:', err);
          this.transactions = [];
        }
      });
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

  // 3. Temizle Butonu İçin
  clearFilters() {
    this.startDate = null;
    this.endDate = null;
    this.applyFilters();
  }

  onAccountChange() {
    // Hesap değişince para birimini güncelle
    const acc = this.accounts.find(x => x.id === this.selectedAccountId);
    this.currentCurrency = acc?.currencyCode || 'TRY';
    
    this.applyFilters();
  }

  // 3. İşlemleri Getir
  loadTransactions(accountId: number) {
    this.isLoading = true;

    this._transactionService.getAll({ accountId: accountId })
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